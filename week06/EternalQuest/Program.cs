using System;
using System.Collections.Generic;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();
        int score = 0;
        string choice = "";

        Console.WriteLine("Welcome to Eternal Quest!");

        while (choice != "6")
        {
            Console.WriteLine();
            Console.WriteLine($"You have {score} points.");
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");

            choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    CreateGoal(goals);
                    break;

                case "2":
                    ListGoals(goals);
                    break;

                case "3":
                    SaveGoals(goals, score);
                    break;

                case "4":
                    LoadGoals(goals, ref score);
                    break;

                case "5":
                    RecordEvent(goals, ref score);
                    break;

                case "6":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine(
                        "Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal(List<Goal> goals)
    {
        Console.WriteLine();
        Console.WriteLine("What type of goal would you like to create?");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter your choice: ");

        string goalType = Console.ReadLine() ?? "";

        if (goalType != "1" &&
            goalType != "2" &&
            goalType != "3")
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine() ?? "";

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine() ?? "";

        Console.Write("How many points is this goal worth? ");

        if (!int.TryParse(Console.ReadLine(), out int points)
            || points < 0)
        {
            Console.WriteLine(
                "Enter a valid non-negative number.");
            return;
        }

        if (goalType == "1")
        {
            goals.Add(
                new SimpleGoal(name, description, points));

            Console.WriteLine(
                "Simple Goal created successfully!");
        }
        else if (goalType == "2")
        {
            goals.Add(
                new EternalGoal(name, description, points));

            Console.WriteLine(
                "Eternal Goal created successfully!");
        }
        else if (goalType == "3")
        {
            Console.Write(
                "How many times must you complete it? ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int targetCount)
                || targetCount <= 0)
            {
                Console.WriteLine(
                    "Enter a number greater than zero.");
                return;
            }

            Console.Write(
                "How many bonus points will you earn? ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int bonusPoints)
                || bonusPoints < 0)
            {
                Console.WriteLine(
                    "Enter a valid non-negative number.");
                return;
            }

            goals.Add(
                new ChecklistGoal(
                    name,
                    description,
                    points,
                    targetCount,
                    bonusPoints));

            Console.WriteLine(
                "Checklist Goal created successfully!");
        }
    }

    static void ListGoals(List<Goal> goals)
    {
        Console.WriteLine();
        Console.WriteLine("Your Goals:");

        if (goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet.");
            return;
        }

        for (int i = 0; i < goals.Count; i++)
        {
            string status =
                goals[i].IsComplete() ? "[X]" : "[ ]";

            Console.WriteLine(
                $"{i + 1}. {status} " +
                $"{goals[i].GetDetailsString()}");
        }
    }

    static void RecordEvent(
        List<Goal> goals,
        ref int score)
    {
        if (goals.Count == 0)
        {
            Console.WriteLine(
                "You have no goals to record.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine(
            "Which goal did you accomplish?");

        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {goals[i].GetName()}");
        }

        Console.Write("Enter the goal number: ");

        if (!int.TryParse(
                Console.ReadLine(),
                out int choice)
            || choice < 1
            || choice > goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal selectedGoal = goals[choice - 1];

        if (selectedGoal is SimpleGoal
            && selectedGoal.IsComplete())
        {
            Console.WriteLine(
                "This Simple Goal is already complete.");
            return;
        }

        if (selectedGoal is ChecklistGoal
            && selectedGoal.IsComplete())
        {
            Console.WriteLine(
                "This Checklist Goal is already complete.");
            return;
        }

        bool wasComplete = selectedGoal.IsComplete();

        selectedGoal.RecordEvent();

        score += selectedGoal.GetPoints();

        Console.WriteLine(
            $"You earned {selectedGoal.GetPoints()} points!");

        if (selectedGoal is ChecklistGoal checklistGoal
            && !wasComplete
            && checklistGoal.IsComplete())
        {
            score += checklistGoal.GetBonus();

            Console.WriteLine(
                $"Congratulations! You earned a " +
                $"{checklistGoal.GetBonus()} point bonus!");
        }

        Console.WriteLine(
            $"Your total score is now {score}.");
    }

    static void SaveGoals(
        List<Goal> goals,
        int score)
    {
        Console.Write(
            "Enter the filename to save your goals: ");

        string filename = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.");
            return;
        }

        try
        {
            using (StreamWriter writer =
                   new StreamWriter(filename))
            {
                writer.WriteLine(score);

                foreach (Goal goal in goals)
                {
                    writer.WriteLine(
                        goal.GetStringRepresentation());
                }
            }

            Console.WriteLine(
                "Goals saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error saving goals: {ex.Message}");
        }
    }

    static void LoadGoals(
        List<Goal> goals,
        ref int score)
    {
        Console.Write(
            "Enter the filename to load your goals: ");

        string filename = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.");
            return;
        }

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0 ||
                !int.TryParse(lines[0], out int loadedScore))
            {
                Console.WriteLine(
                    "The save file is invalid.");
                return;
            }

            List<Goal> loadedGoals =
                new List<Goal>();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');

                switch (parts[0])
                {
                    case "SimpleGoal":
                    {
                        if (parts.Length >= 5 &&
                            int.TryParse(
                                parts[3],
                                out int simplePoints) &&
                            bool.TryParse(
                                parts[4],
                                out bool isComplete))
                        {
                            SimpleGoal simple =
                                new SimpleGoal(
                                    parts[1],
                                    parts[2],
                                    simplePoints);

                            if (isComplete)
                            {
                                simple.RecordEvent();
                            }

                            loadedGoals.Add(simple);
                        }

                        break;
                    }

                    case "EternalGoal":
                    {
                        if (parts.Length >= 4 &&
                            int.TryParse(
                                parts[3],
                                out int eternalPoints))
                        {
                            EternalGoal eternal =
                                new EternalGoal(
                                    parts[1],
                                    parts[2],
                                    eternalPoints);

                            // Restore event count when available.
                            if (parts.Length >= 5 &&
                                int.TryParse(
                                    parts[4],
                                    out int eventCount))
                            {
                                eternal.RestoreEventCount(
                                    eventCount);
                            }

                            loadedGoals.Add(eternal);
                        }

                        break;
                    }

                    case "ChecklistGoal":
                    {
                        if (parts.Length >= 7 &&
                            int.TryParse(
                                parts[3],
                                out int checklistPoints) &&
                            int.TryParse(
                                parts[4],
                                out int targetCount) &&
                            int.TryParse(
                                parts[5],
                                out int currentCount) &&
                            int.TryParse(
                                parts[6],
                                out int bonusPoints))
                        {
                            ChecklistGoal checklist =
                                new ChecklistGoal(
                                    parts[1],
                                    parts[2],
                                    checklistPoints,
                                    targetCount,
                                    bonusPoints);

                            checklist.RestoreProgress(
                                currentCount);

                            loadedGoals.Add(checklist);
                        }

                        break;
                    }
                }
            }

            goals.Clear();
            goals.AddRange(loadedGoals);
            score = loadedScore;

            Console.WriteLine(
                "Goals loaded successfully!");

            Console.WriteLine(
                $"Loaded {goals.Count} goals.");

            Console.WriteLine(
                $"Current score: {score}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error loading goals: {ex.Message}");
        }
    }
}
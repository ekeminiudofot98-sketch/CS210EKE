using System;
using System.Collections.Generic;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        Console.WriteLine("Welcome to Eternal Quest!");
        Console.WriteLine($"Your score is: {_score}");
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"Your current score is: {_score}");
    }

    public void ListGoalNames()
    {
        Console.WriteLine("\nYour Goals:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("\nYour Goals:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine(
                $"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

        public void CreateGoal()
    {
        Console.WriteLine("\nThe types of goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string choice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description? ");
        string description = Console.ReadLine();

        Console.Write("How many points is this goal worth? ");
        if (!int.TryParse(Console.ReadLine(), out int points) || points <= 0)
        {
            Console.WriteLine("Please enter a positive number of points.");
            return;
        }

        Goal goal;

        if (choice == "1")
        {
            goal = new SimpleGoal(name, description, points);
        }
        else if (choice == "2")
        {
            goal = new EternalGoal(name, description, points);
        }
        else if (choice == "3")
        {
            Console.Write("How many times must you complete this goal? ");
            if (!int.TryParse(Console.ReadLine(), out int target) || target <= 0)
            {
                Console.WriteLine("Please enter a positive target.");
                return;
            }

            Console.Write("How many bonus points will you earn? ");
            if (!int.TryParse(Console.ReadLine(), out int bonus) || bonus < 0)
            {
                Console.WriteLine("Please enter zero or a positive bonus.");
                return;
            }

            goal = new ChecklistGoal(
                name, description, points, target, bonus);
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
            return;
        }

        _goals.Add(goal);
        Console.WriteLine("Goal created successfully!");
    }

        public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("You have no goals yet. Create a goal first.");
            return;
        }

        ListGoalDetails();

        Console.Write("Which goal did you accomplish? ");

        if (!int.TryParse(Console.ReadLine(), out int choice) ||
            choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid goal number.");
            return;
        }

        Goal goal = _goals[choice - 1];

        if (goal.IsComplete())
        {
            Console.WriteLine("This goal is already complete.");
            return;
        }

        bool wasComplete = goal.IsComplete();

        goal.RecordEvent();

        _score += goal.GetPoints();

        if (goal is ChecklistGoal checklist &&
            !wasComplete && checklist.IsComplete())
        {
            _score += checklist.GetBonus();

            Console.WriteLine(
                $"Congratulations! You earned {checklist.GetBonus()} bonus points!");
        }

        Console.WriteLine($"You earned {goal.GetPoints()} points!");
        Console.WriteLine($"Your new score is: {_score}");
    }

        public void SaveToFile()
    {
        Console.Write("Enter the filename to save to: ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals and score saved successfully!");
    }

        public void LoadFromFile()
    {
        Console.Write("Enter the filename to load from: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        if (lines.Length == 0)
        {
            Console.WriteLine("The file is empty.");
            return;
        }

        _score = int.Parse(lines[0]);
        _goals.Clear();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            if (parts[0] == "SimpleGoal")
            {
                SimpleGoal goal = new SimpleGoal(
                    parts[1], parts[2], int.Parse(parts[3]));

                if (bool.Parse(parts[4]))
                {
                    goal.RecordEvent();
                }

                _goals.Add(goal);
            }
            else if (parts[0] == "EternalGoal")
            {
                EternalGoal goal = new EternalGoal(
                    parts[1], parts[2], int.Parse(parts[3]));

                _goals.Add(goal);
            }
            else if (parts[0] == "ChecklistGoal")
            {
                Console.WriteLine(
                    "Checklist goals need a little extra restoration code before loading.");
            }
        }

        Console.WriteLine("File loaded. Your score is: " + _score);
    }

}
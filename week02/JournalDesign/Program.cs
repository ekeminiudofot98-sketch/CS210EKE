using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        int choice = 0;

        Console.WriteLine("Welcome to the Journal Program!");

        // Creativity: I added a mood question to each journal entry
        // and included random prompts to make journaling more interesting.

        while (choice != 5)
        {
            Console.WriteLine();
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                string prompt = promptGenerator.GetRandomPrompt();

                Console.WriteLine();
                Console.WriteLine(prompt);
                Console.Write("> ");
                string text = Console.ReadLine();

                Console.Write("How are you feeling today? ");
                string mood = Console.ReadLine();

                string date = DateTime.Now.ToString("dd/MM/yyyy");

                Entry entry = new Entry();

                entry._date = date;
                entry._prompt = prompt;
                entry._text = text + " [Mood: " + mood + "]";

                journal.AddEntry(entry);

                Console.WriteLine("Your entry has been added.");
            }
            else if (choice == 2)
            {
                Console.WriteLine();
                journal.Display();
            }
            else if (choice == 3)
            {
                Console.Write("Enter the filename to load: ");
                string filename = Console.ReadLine();

                journal.LoadFromFile(filename);

                Console.WriteLine("Journal loaded.");
            }
            else if (choice == 4)
            {
                Console.Write("Enter the filename to save: ");
                string filename = Console.ReadLine();

                journal.SaveToFile(filename);

                Console.WriteLine("Journal saved.");
            }
            else if (choice == 5)
            {
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please choose 1-5.");
            }
        }
    }
}
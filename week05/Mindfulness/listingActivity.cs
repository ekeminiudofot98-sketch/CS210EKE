using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts;

    public ListingActivity()
        : base(
            "Listing Activity",
            "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    public void Run()
    {
        StartActivity();

        Random random = new Random();
        int promptIndex = random.Next(_prompts.Count);

        Console.WriteLine();
        Console.WriteLine("List as many responses as you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {_prompts[promptIndex]} ---");
        Console.WriteLine();

        Console.WriteLine("You may begin in:");
        ShowCountdown(5);

        Console.WriteLine();
        Console.WriteLine();

        List<string> items = new List<string>();

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();

            if (DateTime.Now < endTime)
            {
                items.Add(item);
            }
        }

        Console.WriteLine();
        Console.WriteLine($"You listed {items.Count} items.");

        EndActivity();
    }
}
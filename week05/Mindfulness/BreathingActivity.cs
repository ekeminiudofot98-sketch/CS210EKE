using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base(
            "Breathing Activity",
            "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    public void Run()
    {
        StartActivity();

        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine();
            Console.Write("Breathe in...");
            ShowCountdown(4);

            if (DateTime.Now >= endTime)
            {
                break;
            }

            Console.WriteLine();
            Console.Write("Breathe out...");
            ShowCountdown(4);
        }

        EndActivity();
    }
}
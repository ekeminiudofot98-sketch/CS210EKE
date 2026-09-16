using System;

public class PromptGenerator
{
    private List<string> _prompts = new List<string>
    {
        "What did you learn today?",
        "What are you grateful for today?",
        "What was the best part of your day?",
        "What was something difficult you faced today?",
        "What is one goal you have for tomorrow?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(_prompts.Count);

        return _prompts[index];
    }
}
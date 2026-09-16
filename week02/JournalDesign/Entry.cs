public class Entry
{
    public string _date = "";
    public string _prompt = "";
    public string _text = "";

    public string GetDisplayText()
    {
        return $"Date: {_date}\nPrompt: {_prompt}\nEntry: {_text}";
    }
}
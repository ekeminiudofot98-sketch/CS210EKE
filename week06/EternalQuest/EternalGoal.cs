public class EternalGoal : Goal
{
    private int _eventCount;

    public EternalGoal(
        string name,
        string description,
        int points) : base(name, description, points)
    {
        _eventCount = 0;
    }

    public override void RecordEvent()
    {
        _eventCount++;
    }

    public override bool IsComplete()
    {
        return false;
    }

    public override string GetDetailsString()
    {
        return $"{GetName()} ({GetDescription()}) - " +
               $"Points: {GetPoints()} - " +
               $"Events Recorded: {_eventCount}";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{GetName()}|{GetDescription()}|" +
               $"{GetPoints()}|{_eventCount}";
    }

    public int GetEventCount()
    {
        return _eventCount;
    }

    public void RestoreEventCount(int eventCount)
    {
        _eventCount = eventCount;
    }
}
namespace OpenSBS.Engine.Utils;

public class CountdownTimer(TimeSpan value)
{
    private double _original = value.TotalSeconds;

    public double Current { get; private set; } = value.TotalSeconds;
    public double Ratio { get; private set; } = 0;

    public void Reset(TimeSpan value)
    {
        _original = value.TotalSeconds;
        Current = value.TotalSeconds;
        Ratio = 0;
    }

    public bool Advance(TimeSpan deltaT)
    {
        Current -= deltaT.TotalSeconds;
        Ratio = _original > 0 ? (_original - Current) / _original : 0;

        return Current <= 0;
    }
}

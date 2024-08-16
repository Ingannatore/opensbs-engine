namespace OpenSBS.Engine.Utils;

public class BoundedValue(int current, int maximum)
{
    public int Current { get; private set; } = current;
    public int Maximum { get; } = maximum;
    public double Ratio => Current / (double)Maximum;
    public int Missing => Maximum - Current;
    public Boolean IsZero => Current == 0;

    public void Inc(int amount)
    {
        Current = Math.Min(Current + amount, Maximum);
    }

    public void Dec(int amount)
    {
        Current = Math.Max(Current - amount, 0);
    }
}

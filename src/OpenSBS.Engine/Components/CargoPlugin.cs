using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Components;

public class CargoComponent(int size)
{
    private readonly IDictionary<string, int> _content = new Dictionary<string, int>();
    public BoundedValue Space { get; } = new BoundedValue(0, size > 2 ? (int)(10 * Math.Pow(2, size - 3)) : 0);
}


using System.Collections;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Models.Plugins;

public class CargoPlugin(int size) : EntityPlugin, IEnumerable<ItemStack>
{
    private readonly IDictionary<string, int> _content = new Dictionary<string, int>();
    public BoundedValue Space { get; } = new BoundedValue(0, size > 2 ? (int)(10 * Math.Pow(2, size - 3)) : 0);

    public override void OnTick(World world, Celestial owner, TimeSpan deltaT) { }

    public IEnumerator<ItemStack> GetEnumerator() => _content.Select(kvp => new ItemStack(kvp.Key, kvp.Value)).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using System.Collections;

namespace OpenSBS.Engine.Models.Items;

public class ItemCollection : IEnumerable<ItemStack>
{
    private readonly IDictionary<string, ItemStack> _items = new Dictionary<string, ItemStack>();
    public int TotalMass => _items.Values.Sum(item => item.Mass);

    public bool Contains(string itemId) => _items.ContainsKey(itemId);

    public void Add(Item item, int quantity)
    {
        if (quantity <= 0)
        {
            return;
        }

        if (!_items.ContainsKey(item.Id))
        {
            _items.Add(item.Id, ItemStack.Create(item, 0));
        }

        _items[item.Id].Increment(quantity);
    }

    public ItemStack? Extract(string itemId, int quantity)
    {
        if (quantity <= 0 || !_items.ContainsKey(itemId))
        {
            return null;
        }

        var item = _items[itemId];
        if (item.HasNoMoreQuantityThen(quantity))
        {
            _items.Remove(itemId);
            return item;
        }

        return item.Split(quantity);
    }

    public IEnumerator<ItemStack> GetEnumerator()
    {
        return _items.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

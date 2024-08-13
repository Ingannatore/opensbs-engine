namespace OpenSBS.Engine.Utils;

public class RandomBag<T>(Random randomizer, List<T>? items = null)
{
    private readonly IList<T> _items = items ?? new List<T>();
    private readonly Random _randomizer = randomizer;

    public int Count => _items.Count;

    public bool Contains(T item) => _items.Contains(item);

    public void Add(T item, int times = 1)
    {
        for (var i = 0; i < times; i++)
        {
            _items.Add(item);
        }
    }

    public T Draw()
    {
        if (!_items.Any())
        {
            throw new Exception("Unable to draw from an empty RandomBag");
        }

        var index = _randomizer.Next(_items.Count);
        var item = _items[index];
        _items.RemoveAt(index);

        return item;
    }
}

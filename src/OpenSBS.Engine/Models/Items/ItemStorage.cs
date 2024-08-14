namespace OpenSBS.Engine.Models.Items;

public class ItemStorage(int capacity)
{
    public int Capacity { get; } = capacity;
    public ItemCollection Items { get; } = new ItemCollection();

    public static ItemStorage Create(int capacity) => new(capacity);

    public ItemStack? Extract(string itemId, int quantity = 1)
    {
        return Items.Contains(itemId) ? Items.Extract(itemId, quantity) : null;
    }

    public void Add(Entity item, int quantity = 1)
    {
        Items.Add(item, quantity);
    }

    public void Add(ItemStack? stack)
    {
        if (stack != null)
        {
            Items.Add(stack.Item, stack.Quantity);
        }
    }
}

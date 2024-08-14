namespace OpenSBS.Engine.Models.Items;

public class ItemStack(Entity item, int quantity)
{
    public Entity Item { get; } = item;
    public int Quantity { get; private set; } = quantity;
    public bool IsEmpty => Quantity == 0;

    public static ItemStack Create(Entity item, int quantity) => new(item, quantity);

    public bool IsSameItem(ItemStack otherStack)
    {
        return Item.Id == otherStack.Item.Id;
    }

    public bool HasNoMoreQuantityThen(int quantity)
    {
        return Quantity <= quantity;
    }

    public void Increment(int quantity)
    {
        Quantity += quantity;
    }

    public void Decrement(int quantity)
    {
        Quantity = Math.Max(Quantity - quantity, 0);
    }

    public ItemStack Split(int quantity)
    {
        Decrement(quantity);
        return Create(Item, quantity);
    }
}

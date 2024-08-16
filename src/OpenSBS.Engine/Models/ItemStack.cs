namespace OpenSBS.Engine.Models;

public class ItemStack(string itemId, int quantity)
{
    public string ItemId { get; } = itemId;
    public int Quantity { get; } = quantity;
}

namespace OpenSBS.Engine.Models;

public class ItemStack(string id, int quantity)
{
    public string Id { get; } = id;
    public int Quantity { get; } = quantity;
}

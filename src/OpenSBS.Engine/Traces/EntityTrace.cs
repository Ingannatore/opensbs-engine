using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Traces;

public class EntityTrace(string id, string initialCallSign, string[,] signature)
{
    public string Id { get; } = id;
    public int ScanLevel { get; private set; } = 0;
    public string[,] Signature { get; } = signature;
    public string? Type { get; private set; }
    public string CallSign { get; private set; } = initialCallSign;
    public TraceSpatialData Spatial { get; } = new();

    public static EntityTrace ForEntity(Spaceship entity, string initialCallSign, string[,] signature) =>
        new(entity.Id, initialCallSign, signature);

    public bool IsOutOfRange(int range) => Spatial.IsOutOfRange(range);

    public void IncreaseScanLevel(int amount = 1)
    {
        ScanLevel += amount;
    }

    public void Update(Spaceship owner, Spaceship target)
    {
        Spatial.Update(owner, target);
        if (ScanLevel < 1)
        {
            return;
        }

        Type = "whatever"; // TODO: temporary, to be replaced
    }
}

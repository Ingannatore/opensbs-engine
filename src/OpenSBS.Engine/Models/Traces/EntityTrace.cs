using OpenSBS.Engine.Modules.Shields;

namespace OpenSBS.Engine.Models.Traces;

public class EntityTrace(string id, string initialCallSign, string[,] signature)
{
    public string Id { get; } = id;
    public int ScanLevel { get; private set; } = 0;
    public string[,] Signature { get; } = signature;
    public string? Type { get; private set; }
    public string CallSign { get; private set; } = initialCallSign;
    public TraceSpatialData Spatial { get; } = new();
    public TraceShieldData? Shield { get; private set; }

    public static EntityTrace ForEntity(SpaceEntity entity, string initialCallSign, string[,] signature) =>
        new(entity.Id, initialCallSign, signature);

    public bool IsOutOfRange(int range) => Spatial.IsOutOfRange(range);
    public bool IsOutOfFiringArc(IEnumerable<string> firingArcs) => Spatial.IsOutOfFiringArc(firingArcs);

    public void IncreaseScanLevel(int amount = 1)
    {
        ScanLevel += amount;
    }

    public void Update(SpaceEntity owner, SpaceEntity target)
    {
        Spatial.Update(owner, target);
        if (ScanLevel < 1)
        {
            return;
        }

        var shieldModule = target.Modules.FirstOrDefault<ShieldModule>();
        if (shieldModule != null)
        {
            Shield ??= new TraceShieldData();
            Shield.Update(shieldModule);
        }

        Type = target.Type;
    }
}

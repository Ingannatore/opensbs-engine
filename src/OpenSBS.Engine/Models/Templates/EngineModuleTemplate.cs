namespace OpenSBS.Engine.Models.Templates;

public abstract class EngineModuleTemplate(string id, string name, int size) : ModuleTemplate(id, name, size)
{
    public int MaximumSpeed { get; protected set; }
    public int Acceleration { get; protected set; }
    public int Deceleration { get; protected set; }
    public int RotationSpeed { get; protected set; }
}

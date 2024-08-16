namespace OpenSBS.Engine.Models.Templates;

public abstract class ShieldModuleTemplate(string id, string name, int size) : ModuleTemplate(id, name, size)
{
    public int Capacity { get; protected set; }
    public int RechargeRate { get; protected set; }
}

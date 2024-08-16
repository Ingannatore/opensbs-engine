namespace OpenSBS.Engine.Models.Templates;

public abstract class WeaponModuleTemplate(string id, string name, int size) : ModuleTemplate(id, name, size)
{
    public int Damage { get; protected set; }
    public int Range { get; protected set; }
    public int AmmoPerCycle { get; protected set; }
    public int MagazineSize { get; protected set; }
    public int CycleTime { get; protected set; }
    public int ReloadTime { get; protected set; }
}

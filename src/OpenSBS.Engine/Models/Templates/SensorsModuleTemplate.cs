namespace OpenSBS.Engine.Models.Templates;

public class SensorsModuleTemplate(string id, string name, int size) : ModuleTemplate(id, name, size)
{
    public int Range { get; protected set; }
}

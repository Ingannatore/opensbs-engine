using System.Text.Json;

namespace OpenSBS.Engine.Commands;

public abstract class Command(string type, object payload)
{
    public readonly string Type = type;
    public readonly string Payload = JsonSerializer.Serialize(payload);

    public T? PayloadTo<T>() => JsonSerializer.Deserialize<T>(Payload);
}

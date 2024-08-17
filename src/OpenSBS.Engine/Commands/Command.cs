using System.Text.Json;

namespace OpenSBS.Engine.Commands;

public class Command(string type, object payload, CommandMetadata? meta = null)
{
    public readonly string Type = type;
    public readonly string Payload = JsonSerializer.Serialize(payload);
    public readonly CommandMetadata? Meta = meta;
    public readonly bool HasMetadata = meta != null;
    public readonly bool IsServerRecipient = meta == null || meta.IsServerRecipient;

    public T? PayloadTo<T>() => JsonSerializer.Deserialize<T>(Payload);
}

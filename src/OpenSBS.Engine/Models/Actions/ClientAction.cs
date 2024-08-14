using System.Text.Json;

namespace OpenSBS.Engine.Models.Actions;

public class ClientAction(string type, object payload)
{
    public string Type { get; set; } = type;
    public string Payload { get; set; } = JsonSerializer.Serialize(payload);
    public ClientActionMetadata? Meta { get; set; }

    public bool IsServerRecipient() => Meta == null || Meta.IsServerRecipient();
    public T? PayloadTo<T>() => JsonSerializer.Deserialize<T>(Payload);
}

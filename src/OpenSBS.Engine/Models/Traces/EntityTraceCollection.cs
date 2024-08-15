using System.Collections;

namespace OpenSBS.Engine.Models.Traces;

public class EntityTraceCollection : IEnumerable<EntityTrace>
{
    private readonly Random _randomizer;
    private readonly SignatureGenerator _signatureGenerator;
    private readonly IDictionary<string, EntityTrace> _traces = new Dictionary<string, EntityTrace>();

    public EntityTraceCollection()
    {
        _randomizer = new Random();
        _signatureGenerator = new(_randomizer);
    }

    public EntityTrace? Get(string entityId) => _traces.ContainsKey(entityId) ? _traces[entityId] : null;
    public void Remove(string entityId) => _traces.Remove(entityId);

    public IEnumerator<EntityTrace> GetEnumerator() => _traces.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void CompleteScansion(string entityId)
    {
        if (_traces.TryGetValue(entityId, out var trace))
        {
            trace.IncreaseScanLevel();
        }
    }

    public void Update(Celestial owner, Celestial target, int range)
    {
        if (!_traces.ContainsKey(target.Id))
        {
            _traces[target.Id] = EntityTrace.ForEntity(
                entity: target,
                initialCallSign: $"X-{_randomizer.Next(1, 99999):00000}",
                signature: _signatureGenerator.Generate()
            );
        }

        _traces[target.Id].Update(owner, target);
        if (_traces[target.Id].IsOutOfRange(range))
        {
            // TODO: Wrong! Should be marked as out-of-range without losing any data
            _traces.Remove(target.Id);
        }
    }
}

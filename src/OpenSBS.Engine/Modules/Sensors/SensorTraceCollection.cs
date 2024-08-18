using System.Collections;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorTraceCollection : IEnumerable<SensorTrace>
{
    private readonly IDictionary<string, SensorTrace> _traces = new Dictionary<string, SensorTrace>();

    public SensorTrace? Get(string id) => _traces.ContainsKey(id) ? _traces[id] : null;

    public void OnTick(Spaceship owner, Dictionary<string, Spaceship> spaceships)
    {
        _traces
            .Where(pair => !spaceships.ContainsKey(pair.Key) && !pair.Value.IsOutOfRange)
            .Select(pair => pair.Value).ToList()
            .ForEach(spaceship => spaceship.MarkAsOutOfRange());

        foreach (var (id, spaceship) in spaceships)
        {
            if (!_traces.ContainsKey(id))
            {
                _traces[id] = new SensorTrace(id);
            }

            _traces[id].Update(owner, spaceship);
        }
    }

    public IEnumerator<SensorTrace> GetEnumerator() => _traces.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

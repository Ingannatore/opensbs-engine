using System.Collections;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Modules.Sensors;

public class SensorTraceCollection : IEnumerable<SensorTrace>
{
    private readonly IDictionary<string, SensorTrace> _traces = new Dictionary<string, SensorTrace>();

    public int CountLockedTargets => _traces.Values.Where(trace => trace.IsLockingOrLocked).Count();
    public SensorTrace? Get(string id) => _traces.ContainsKey(id) ? _traces[id] : null;

    public void OnTick(Dictionary<string, Spaceship> spaceships, Spaceship owner, SensorsModule module, TimeSpan deltaT)
    {
        _traces
            .Where(pair => !spaceships.ContainsKey(pair.Key) && !pair.Value.IsOutOfRange)
            .Select(pair => pair.Value).ToList()
            .ForEach(trace => trace.MarkAsOutOfRange());

        foreach (var (id, spaceship) in spaceships)
        {
            if (!_traces.ContainsKey(id))
            {
                _traces[id] = new SensorTrace(spaceship);
            }

            _traces[id].OnTick(spaceship, owner, deltaT);
        }
    }

    public IEnumerator<SensorTrace> GetEnumerator() => _traces.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

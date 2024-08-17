using System.Collections;
using OpenSBS.Engine.Models;

namespace OpenSBS.Engine
{
    public class World : IEnumerable<Celestial>
    {
        private readonly IDictionary<string, Celestial> _entities = new Dictionary<string, Celestial>();

        public bool ExistsEntity(string id) => _entities.ContainsKey(id);
        public Celestial GetEntity(string id) => _entities[id];
        public void AddEntity(Celestial entity) => _entities[entity.Id] = entity;
        //public void DamageEntity(string id, int amount) => _entities[id].ApplyDamage(amount);

        public void Update(TimeSpan deltaT)
        {
            foreach (var entity in _entities.Values)
            {
                // if (entity.IsDestroyed)
                // {
                //     _entities.Remove(entity.Id);
                //     continue;
                // }

                entity.OnTick(this, entity, deltaT);
            }
        }

        public IEnumerator<Celestial> GetEnumerator() => _entities.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

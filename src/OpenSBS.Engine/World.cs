using System.Collections;
using OpenSBS.Engine.Models;

namespace OpenSBS.Engine
{
    public class World : IEnumerable<SpaceEntity>
    {
        private readonly IDictionary<string, SpaceEntity> _entities = new Dictionary<string, SpaceEntity>();

        public bool ExistsEntity(string id) => _entities.ContainsKey(id);
        public SpaceEntity GetEntity(string id) => _entities[id];
        public void AddEntity(SpaceEntity entity) => _entities[entity.Id] = entity;
        public void DamageEntity(string id, int amount) => _entities[id].ApplyDamage(amount);

        public void Update(TimeSpan deltaT)
        {
            foreach (var entity in _entities.Values)
            {
                if (entity.IsDestroyed)
                {
                    _entities.Remove(entity.Id);
                    continue;
                }

                entity.Update(deltaT, this);
            }
        }

        public IEnumerator<SpaceEntity> GetEnumerator() => _entities.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

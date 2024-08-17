using System.Collections;
using OpenSBS.Engine.Behaviours;
using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine
{
    public class World : IEnumerable<Entity>
    {
        private readonly Dictionary<string, Entity> _entities = [];
        private readonly Dictionary<string, ITickable> _tickables = [];

        public bool Exists(string id) => _entities.ContainsKey(id);
        public Entity Get(string id) => _entities[id];

        public void Add(Entity entity)
        {
            _entities[entity.Id] = entity;

            if (entity is ITickable tickableEntity)
            {
                _tickables[entity.Id] = tickableEntity;
            }
        }

        public void Remove(string id)
        {
            _entities.Remove(id);
            _tickables.Remove(id);
        }

        public void OnTick(TimeSpan deltaT)
        {
            foreach (var entity in _tickables.Values)
            {
                entity.OnTick(this, (Entity)entity, deltaT);
            }
        }

        public IEnumerator<Entity> GetEnumerator() => _entities.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

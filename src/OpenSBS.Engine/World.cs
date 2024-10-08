﻿using System.Collections;
using System.Numerics;
using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine
{
    public class World : IEnumerable<Entity>
    {
        private readonly Dictionary<string, Entity> _entities = [];
        private readonly Dictionary<string, Spaceship> _spaceships = [];

        public bool Exists(string id) => _entities.ContainsKey(id);
        public Entity Get(string id) => _entities[id];
        public Dictionary<string, Spaceship> FindSpaceships(Vector3 center, int range) =>
            _spaceships
                .Where(pair => Vectors.Distance(center, pair.Value.Body.Position) <= range)
                .ToDictionary();

        public void Add(Entity entity)
        {
            _entities[entity.Id] = entity;

            if (entity is Spaceship spaceship)
            {
                _spaceships[entity.Id] = spaceship;
            }
        }

        public void Remove(string id)
        {
            _entities.Remove(id);
            _spaceships.Remove(id);
        }

        public void OnTick(TimeSpan deltaT)
        {
            foreach (var spaceship in _spaceships.Values)
            {
                spaceship.OnTick(this, deltaT);
            }
        }

        public IEnumerator<Entity> GetEnumerator() => _entities.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

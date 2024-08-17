using System.Numerics;
using OpenSBS.Engine.Components;

namespace OpenSBS.Engine.Entities;

public class Celestial(string id, string name, Vector3 position) : Entity(id, name)
{
    public readonly BodyComponent Body = new(position);

    public override void OnTick(World world, Entity owner, TimeSpan deltaT)
    {
        Body.OnTick(world, this, deltaT);
    }
}

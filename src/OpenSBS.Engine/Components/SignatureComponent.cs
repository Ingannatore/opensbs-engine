using OpenSBS.Engine.Entities;

namespace OpenSBS.Engine.Components;

public class SignatureComponent(int size)
{
    public int Erm { get; protected set; } = size;
    public int Mag { get; protected set; } = size;
    public int Grav { get; protected set; } = size;

    public void OnTick(Spaceship owner)
    {
        UpdateErmSignature(owner);
        UpdateMagSignature(owner);
    }

    private void UpdateErmSignature(Spaceship owner)
    {
        // TODO: based on the spaceship energy usage
    }

    private void UpdateMagSignature(Spaceship owner)
    {
        // TODO: based on the spaceship shield usage
        // TODO: based on the spaceship armour
    }

    private void UpdateGravSignature(Spaceship owner)
    {
        // TODO: based on the spaceship FTL engine usage
    }
}

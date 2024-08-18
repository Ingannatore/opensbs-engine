using OpenSBS.Engine.Entities;
using OpenSBS.Engine.Utils;

namespace OpenSBS.Engine.Components;

public class SignatureComponent(int size) : Spectra(size)
{
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

namespace OpenSBS.Engine.Utils;

public class Spectra(int erm, int mag, int grav)
{
    public Spectra(int value) : this(value, value, value) { }

    public int Erm { get; protected set; } = erm;
    public int Mag { get; protected set; } = mag;
    public int Grav { get; protected set; } = grav;
}

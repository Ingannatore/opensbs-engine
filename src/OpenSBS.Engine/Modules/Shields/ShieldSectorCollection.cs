using System.Collections;
using OpenSBS.Engine.Models.Entities;

namespace OpenSBS.Engine.Modules.Shields;

public class ShieldSectorCollection(int capacity, int rechargeRate) : IEnumerable<ShieldSector>
{
    private const int MaximumCalibrationPoints = 12;

    private readonly IDictionary<string, ShieldSector> _sectors = new Dictionary<string, ShieldSector>
    {
        {
            EntitySide.Front,
            new ShieldSector(EntitySide.Front, capacity, rechargeRate)
        },
        {
            EntitySide.Left,
            new ShieldSector(EntitySide.Left, capacity, rechargeRate)
        },
        {
            EntitySide.Right,
            new ShieldSector(EntitySide.Right, capacity, rechargeRate)
        },
        {
            EntitySide.Rear,
            new ShieldSector(EntitySide.Rear, capacity, rechargeRate)
        }
    };

    public double GetSectorRatio(string side) => _sectors[side].Ratio;

    public int GetAvailableCalibrationPoints() =>
        MaximumCalibrationPoints - _sectors.Values.Sum(sector => sector.Calibration);

    public void SetCalibration(CalibrationPayload payload) =>
        _sectors[payload.Side].SetCalibration(payload.Value, GetAvailableCalibrationPoints());

    public void ResetCalibration()
    {
        foreach (var sector in _sectors.Values)
        {
            sector.ResetCalibration();
        }
    }

    public void Reinforce(string side)
    {
        switch (side)
        {
            case EntitySide.Front:
                _sectors[EntitySide.Front].ResetCalibration(5);
                _sectors[EntitySide.Left].ResetCalibration();
                _sectors[EntitySide.Right].ResetCalibration();
                _sectors[EntitySide.Rear].ResetCalibration(1);
                break;
            case EntitySide.Left:
                _sectors[EntitySide.Front].ResetCalibration();
                _sectors[EntitySide.Left].ResetCalibration(5);
                _sectors[EntitySide.Right].ResetCalibration(1);
                _sectors[EntitySide.Rear].ResetCalibration();
                break;
            case EntitySide.Right:
                _sectors[EntitySide.Front].ResetCalibration();
                _sectors[EntitySide.Left].ResetCalibration(1);
                _sectors[EntitySide.Right].ResetCalibration(5);
                _sectors[EntitySide.Rear].ResetCalibration();
                break;
            case EntitySide.Rear:
                _sectors[EntitySide.Front].ResetCalibration(1);
                _sectors[EntitySide.Left].ResetCalibration();
                _sectors[EntitySide.Right].ResetCalibration();
                _sectors[EntitySide.Rear].ResetCalibration(5);
                break;
        }
    }

    public void Update()
    {
        foreach (var sector in _sectors.Values)
        {
            sector.Update();
        }
    }

    public IEnumerator<ShieldSector> GetEnumerator() => _sectors.Values.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

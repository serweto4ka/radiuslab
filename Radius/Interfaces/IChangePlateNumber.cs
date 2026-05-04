using System;

namespace Radius.Interfaces;

public interface IChangePlateNumber
{
    void ChangePlateNumber(Guid vehicleId, string newPlate);
}
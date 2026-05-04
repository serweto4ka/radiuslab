using System;

namespace Radius.Interfaces;

public interface IChangeFuelType
{
    void ChangeFuelType(Guid vehicleId, string newFuelType);
}
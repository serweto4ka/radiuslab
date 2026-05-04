using System;

namespace Radius.Interfaces;

public interface IChangeVinCode
{
    void ChangeVinCode(Guid vehicleId, string newVin);
}
using System;

namespace Radius.Interfaces;

public interface IChangeTransmission
{
    void ChangeTransmission(Guid vehicleId, string newTransmission);
}
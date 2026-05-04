using System;

namespace Radius.Interfaces;

public interface IChangeBodyType
{
    void ChangeBodyType(Guid vehicleId, string newBodyType);
}
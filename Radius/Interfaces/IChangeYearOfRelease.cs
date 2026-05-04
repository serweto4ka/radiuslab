using System;

namespace Radius.Interfaces;

public interface IChangeYearOfRelease
{
    void ChangeYearOfRelease(Guid vehicleId, int newYear);
}
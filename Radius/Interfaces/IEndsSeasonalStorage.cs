using System;

namespace Radius.Interfaces;

public interface IEndsSeasonalStorage
{
    void EndStorage(Guid storageId);
}
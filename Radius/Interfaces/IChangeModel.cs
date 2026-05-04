using System;

namespace Radius.Interfaces;

public interface IChangeModel
{
    void ChangeModel(Guid vehicleId, string newModel);
}
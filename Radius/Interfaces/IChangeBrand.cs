using System;

namespace Radius.Interfaces;

public interface IChangeBrand
{
    void ChangeBrand(Guid vehicleId, string newBrand);
}
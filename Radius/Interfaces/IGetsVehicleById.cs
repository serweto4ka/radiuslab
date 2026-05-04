using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsVehicleById
{
    Vehicle GetVehicleById(Guid vehicleId);
}
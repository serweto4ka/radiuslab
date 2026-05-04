using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IVehicleService
{
    void AddVehicle(Vehicle vehicle);
    void RemoveVehicle(Vehicle vehicle);
    IEnumerable<Vehicle> GetAllVehicles();
    Vehicle? GetVehicleById(Guid vehicleId);
    void UpdateVehicle(Vehicle vehicle);
    void ChangePlateNumber(Guid vehicleId, string newPlate);
    void ChangeVinCode(Guid vehicleId, string newVin);
    void ChangeBodyType(Guid vehicleId, string newBodyType);
    void ChangeFuelType(Guid vehicleId, string newFuelType);
    void ChangeTransmission(Guid vehicleId, string newTransmission);
}

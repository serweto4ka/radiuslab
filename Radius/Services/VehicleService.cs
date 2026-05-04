using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Radius.Interfaces;
using Radius.Models;

namespace Radius.Services;

public class VehicleService :
    IAddsVehicle,
    IRemovesVehicle,
    IGetsAllVehicles,
    IGetsVehicleById,
    IUpdatesVehicle,
    IChangePlateNumber,
    IChangeVinCode,
    IChangeBodyType,
    IChangeFuelType,
    IChangeTransmission
{
    private const string DebugLogPath = @"C:\Users\maksi\Desktop\Radius3-master-master\debug-00c82d.log";
    // Тимчасова локальна база даних у пам'яті (доки не підключимо SQLite або API)
    private readonly List<Vehicle> _vehicles = new();

    private static void WriteDebugLog(string runId, string hypothesisId, string location, string message, object data)
    {
        var payload = new
        {
            sessionId = "00c82d",
            runId,
            hypothesisId,
            location,
            message,
            data,
            timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
        try
        {
            File.AppendAllText(DebugLogPath, JsonSerializer.Serialize(payload) + Environment.NewLine);
        }
        catch
        {
        }
    }

    public void AddVehicle(Vehicle vehicle)
    {
        // #region agent log
        WriteDebugLog(
            "initial-debug",
            "H3",
            "VehicleService.cs:AddVehicle",
            "AddVehicle invoked",
            new { isNull = vehicle == null, currentCount = _vehicles.Count });
        // #endregion

        if (vehicle != null)
        {
            _vehicles.Add(vehicle);
            // #region agent log
            WriteDebugLog(
                "initial-debug",
                "H3",
                "VehicleService.cs:AddVehicle",
                "Vehicle added to collection",
                new { vehicleId = vehicle.Id, updatedCount = _vehicles.Count });
            // #endregion
        }
    }

    public void RemoveVehicle(Vehicle vehicle)
    {
        if (vehicle != null)
        {
            _vehicles.Remove(vehicle);
        }
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        return _vehicles;
    }

    public Vehicle GetVehicleById(Guid vehicleId)
    {
        // #region agent log
        WriteDebugLog(
            "initial-debug",
            "H4",
            "VehicleService.cs:GetVehicleById",
            "GetVehicleById lookup",
            new { vehicleId, currentCount = _vehicles.Count });
        // #endregion
        return _vehicles.FirstOrDefault(v => v.Id == vehicleId);
    }

    public void UpdateVehicle(Vehicle vehicle)
    {
        var existingVehicle = GetVehicleById(vehicle.Id);
        if (existingVehicle != null)
        {
            existingVehicle.Brand = vehicle.Brand;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.Year = vehicle.Year;
            // Інші базові поля можна також оновлювати тут
        }
    }

    public void ChangePlateNumber(Guid vehicleId, string newPlate)
    {
        var vehicle = GetVehicleById(vehicleId);
        if (vehicle != null)
        {
            vehicle.LicensePlate = newPlate;
        }
    }

    public void ChangeVinCode(Guid vehicleId, string newVin)
    {
        var vehicle = GetVehicleById(vehicleId);
        if (vehicle != null)
        {
            vehicle.VinCode = newVin;
        }
    }

    public void ChangeBodyType(Guid vehicleId, string newBodyType)
    {
        var vehicle = GetVehicleById(vehicleId);
        if (vehicle != null)
        {
            vehicle.BodyType = newBodyType;
        }
    }

    public void ChangeFuelType(Guid vehicleId, string newFuelType)
    {
        var vehicle = GetVehicleById(vehicleId);
        if (vehicle != null)
        {
            vehicle.FuelType = newFuelType;
        }
    }

    public void ChangeTransmission(Guid vehicleId, string newTransmission)
    {
        var vehicle = GetVehicleById(vehicleId);
        if (vehicle != null)
        {
            vehicle.Transmission = newTransmission;
        }
    }
}
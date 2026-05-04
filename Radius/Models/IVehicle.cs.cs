using System;

namespace Radius.Models;

/// <summary>
/// Інтерфейс транспортного засобу.
/// </summary>
public interface IVehicle
{
    Guid Id { get; }
    string Brand { get; }
    string Model { get; }
    string LicensePlate { get; }
    int Year { get; }
    string VinCode { get; }
}
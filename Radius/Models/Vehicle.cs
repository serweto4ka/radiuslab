using System;

namespace Radius.Models;

/// <summary>
/// Транспортний засіб клієнта.
/// </summary>
public class Vehicle : IVehicle
{
    public Guid Id { get; init; } = Guid.NewGuid();

    private string _brand = string.Empty;
    public string Brand
    {
        get => _brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Марка автомобіля не може бути порожньою.");
            _brand = value.Trim();
        }
    }

    private string _model = string.Empty;
    public string Model
    {
        get => _model;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Модель автомобіля не може бути порожньою.");
            _model = value.Trim();
        }
    }

    private string _licensePlate = string.Empty;
    public string LicensePlate
    {
        get => _licensePlate;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номерний знак не може бути порожнім.");
            _licensePlate = value.Trim().ToUpperInvariant();
        }
    }

    private int _year;
    public int Year
    {
        get => _year;
        set
        {
            int currentYear = DateTime.UtcNow.Year;
            if (value < 1900 || value > currentYear + 1)
                throw new ArgumentOutOfRangeException(nameof(value), $"Рік випуску повинен бути між 1900 і {currentYear + 1}.");
            _year = value;
        }
    }

    private string _vinCode = string.Empty;
    public string VinCode
    {
        get => _vinCode;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("VIN-код не може бути порожнім.");
            if (value.Trim().Length != 17)
                throw new ArgumentException("VIN-код повинен містити рівно 17 символів.");
            _vinCode = value.Trim().ToUpperInvariant();
        }
    }

    // ── Додаткові поля

    public string BodyType { get; set; } = string.Empty;   // Sedan, SUV, Hatchback…
    public string FuelType { get; set; } = string.Empty;   // Petrol, Diesel, Electric…
    public string Transmission { get; set; } = string.Empty;   // Manual, Automatic, CVT…

    // ── Конструктор

    public Vehicle() { }

    public Vehicle(string brand, string model, string licensePlate, int year, string vinCode)
    {
        Brand = brand;
        Model = model;
        LicensePlate = licensePlate;
        Year = year;
        VinCode = vinCode;
    }

    public override string ToString() => $"{Brand} {Model} {Year} · {LicensePlate}";
}
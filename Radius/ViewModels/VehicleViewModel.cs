using System;
using System.Collections.ObjectModel;
using Radius.Models;

namespace Radius.ViewModels;

/// <summary>
/// ViewModel для управління транспортними засобами клієнтів.
/// </summary>
public class VehicleViewModel : BaseViewModel
{
    public ObservableCollection<Vehicle> Vehicles { get; } = new();

    // ── Додавання ───────────────────────────────────────────────────────────

    /// <summary>
    /// Валідує дані та додає новий автомобіль до колекції.
    /// </summary>
    public void AddVehicle(string brand, string model, string licensePlate,
                           string vinCode, int year,
                           string bodyType = "", string fuelType = "", string transmission = "")
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var error =
                Validators.ValidateNotEmpty(brand, "Марка") ??
                Validators.ValidateNotEmpty(model, "Модель") ??
                Validators.ValidateLicensePlate(licensePlate) ??
                Validators.ValidateVin(vinCode) ??
                Validators.ValidateYear(year);

            if (error is not null)
            {
                SetError(error);
                return;
            }

            // Дублікат за VIN
            if (ExistsByVin(vinCode))
            {
                SetError($"Автомобіль з VIN {vinCode.Trim().ToUpperInvariant()} вже зареєстрований.");
                return;
            }

            var vehicle = new Vehicle(
                brand: brand.Trim(),
                model: model.Trim(),
                licensePlate: licensePlate.Trim().ToUpperInvariant(),
                year: year,
                vinCode: vinCode.Trim().ToUpperInvariant()
            )
            {
                BodyType = bodyType.Trim(),
                FuelType = fuelType.Trim(),
                Transmission = transmission.Trim()
            };

            Vehicles.Add(vehicle);
            IsEmpty = false;
            ClearError();
        }
        catch (Exception ex)
        {
            SetError(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    // ── Видалення ───────────────────────────────────────────────────────────

    public bool RemoveVehicle(Guid id)
    {
        var vehicle = GetById(id);
        if (vehicle is null) return false;

        Vehicles.Remove(vehicle);
        IsEmpty = Vehicles.Count == 0;
        return true;
    }

    // ── Пошук ───────────────────────────────────────────────────────────────

    public Vehicle? GetById(Guid id)
        => Vehicles.FirstOrDefault(v => v.Id == id);

    public Vehicle? GetByLicensePlate(string plate)
        => Vehicles.FirstOrDefault(v => v.LicensePlate == plate.Trim().ToUpperInvariant());

    public bool ExistsByVin(string vin)
        => Vehicles.Any(v => v.VinCode == vin.Trim().ToUpperInvariant());
}
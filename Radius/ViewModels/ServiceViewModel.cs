using System;
using System.Collections.ObjectModel;
using Radius.Models;

namespace Radius.ViewModels;

/// <summary>
/// ViewModel для управління послугами шинного центру.
/// </summary>
public class ServiceViewModel : BaseViewModel
{
    public ObservableCollection<Service> Services { get; } = new();

    // ── Додавання ───────────────────────────────────────────────────────────

    /// <summary>
    /// Валідує дані та додає нову послугу до колекції.
    /// </summary>
    public void AddService(string name, decimal price, int durationMinutes, string description = "")
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var error =
                Validators.ValidateNotEmpty(name, "Назва послуги") ??
                Validators.ValidatePositiveDecimal(price, "Ціна") ??
                Validators.ValidatePositiveInt(durationMinutes, "Тривалість");

            if (error is not null)
            {
                SetError(error);
                return;
            }

            // Дублікат за назвою
            if (ExistsByName(name))
            {
                SetError($"Послуга з назвою '{name.Trim()}' вже існує.");
                return;
            }

            var service = new Service(
                name: name.Trim(),
                price: price,
                durationMinutes: durationMinutes,
                description: description.Trim()
            );

            Services.Add(service);
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

    // ── Деактивація / видалення ─────────────────────────────────────────────

    /// <summary>
    /// М'яке видалення: позначає послугу як неактивну (не видаляє з БД).
    /// </summary>
    public bool DeactivateService(Guid id)
    {
        var service = GetById(id);
        if (service is null) return false;

        service.IsActive = false;
        return true;
    }

    public bool RemoveService(Guid id)
    {
        var service = GetById(id);
        if (service is null) return false;

        Services.Remove(service);
        IsEmpty = Services.Count == 0;
        return true;
    }

    // ── Пошук ───────────────────────────────────────────────────────────────

    public Service? GetById(Guid id)
        => Services.FirstOrDefault(s => s.Id == id);

    public bool ExistsByName(string name)
        => Services.Any(s => string.Equals(s.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Service> GetActive()
        => Services.Where(s => s.IsActive);
}
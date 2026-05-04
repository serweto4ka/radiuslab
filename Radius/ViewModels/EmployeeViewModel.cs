using System;
using System.Collections.ObjectModel;
using Radius.Models;

namespace Radius.ViewModels;

/// <summary>
/// ViewModel для управління співробітниками.
/// </summary>
public class EmployeeViewModel : BaseViewModel
{
    public ObservableCollection<Employee> Employees { get; } = new();

    // ── Додавання ───────────────────────────────────────────────────────────

    /// <summary>
    /// Валідує дані та додає нового співробітника до колекції.
    /// </summary>
    public void AddEmployee(string name, string phone, string position, int experienceYears)
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var error =
                Validators.ValidateNotEmpty(name, "Ім'я") ??
                Validators.ValidateNotEmpty(position, "Посада") ??
                Validators.ValidatePhone(phone) ??
                ValidateExperience(experienceYears);

            if (error is not null)
            {
                SetError(error);
                return;
            }

            // Дублікат за телефоном
            if (ExistsByPhone(phone))
            {
                SetError($"Співробітник із номером {phone} вже існує.");
                return;
            }

            var employee = new Employee(
                name: name.Trim(),
                phone: phone.Trim(),
                position: position.Trim(),
                experienceYears: experienceYears
            );

            Employees.Add(employee);
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

    // ── Зміна стану ─────────────────────────────────────────────────────────

    /// <summary>
    /// Перемикає доступність співробітника.
    /// </summary>
    public bool ToggleAvailability(Guid id)
    {
        var employee = GetById(id);
        if (employee is null) return false;

        employee.SetAvailability(!employee.IsAvailable);
        return true;
    }

    // ── Видалення ───────────────────────────────────────────────────────────

    public bool RemoveEmployee(Guid id)
    {
        var employee = GetById(id);
        if (employee is null) return false;

        Employees.Remove(employee);
        IsEmpty = Employees.Count == 0;
        return true;
    }

    // ── Пошук ───────────────────────────────────────────────────────────────

    public Employee? GetById(Guid id)
        => Employees.FirstOrDefault(e => e.Id == id);

    public bool ExistsByPhone(string phone)
        => Employees.Any(e => e.Phone == phone.Trim());

    public IEnumerable<Employee> GetAvailable()
        => Employees.Where(e => e.IsAvailable);

    // ── Приватна валідація ───────────────────────────────────────────────────

    private static string? ValidateExperience(int years)
    {
        if (years < 0) return "Стаж не може бути від'ємним.";
        if (years > 60) return "Стаж не може перевищувати 60 років.";
        return null;
    }
}
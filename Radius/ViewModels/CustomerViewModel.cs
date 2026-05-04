using System;
using System.Collections.ObjectModel;
using Radius.Models;

namespace Radius.ViewModels;

/// <summary>
/// ViewModel для управління клієнтами.
/// </summary>
public class CustomerViewModel : BaseViewModel
{
    public ObservableCollection<Customer> Customers { get; } = new();

    // ── Додавання ───────────────────────────────────────────────────────────

    /// <summary>
    /// Валідує дані та додає нового клієнта до колекції.
    /// </summary>
    public void AddCustomer(string name, string passwordHash, string email, string phone)
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            // Валідація
            var error =
                Validators.ValidateNotEmpty(name, "Ім'я") ??
                Validators.ValidatePhone(phone) ??
                Validators.ValidateEmail(email) ??
                Validators.ValidatePassword(passwordHash);

            if (error is not null)
            {
                SetError(error);
                return;
            }

            // Дублікат за телефоном
            if (ExistsByPhone(phone))
            {
                SetError($"Клієнт із номером {phone} вже зареєстрований.");
                return;
            }

            var customer = new Customer(
                name: name.Trim(),
                passwordHash: passwordHash,
                email: email.Trim(),
                phone: phone.Trim()
            );

            Customers.Add(customer);
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

    /// <summary>
    /// Видаляє клієнта за Id. Повертає true якщо знайдено і видалено.
    /// </summary>
    public bool RemoveCustomer(Guid id)
    {
        var customer = GetById(id);
        if (customer is null) return false;

        Customers.Remove(customer);
        IsEmpty = Customers.Count == 0;
        return true;
    }

    // ── Пошук ───────────────────────────────────────────────────────────────

    public Customer? GetById(Guid id)
        => Customers.FirstOrDefault(c => c.Id == id);

    public Customer? GetByPhone(string phone)
        => Customers.FirstOrDefault(c => c.Phone == phone.Trim());

    public bool ExistsByPhone(string phone)
        => GetByPhone(phone) is not null;
}
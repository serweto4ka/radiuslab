using System;

namespace Radius.Models;

/// <summary>
/// Клієнт шинного центру Radius.
/// Зберігає контактні дані, суму витрат і поточну знижку.
/// </summary>
public class Customer : ICustomer
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime RegistrationDate { get; init; } = DateTime.UtcNow;

    // ── Персональні дані ────────────────────────────────────────────────────

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ім'я клієнта не може бути порожнім.");
            _name = value.Trim();
        }
    }

    private string _phone = string.Empty;
    public string Phone
    {
        get => _phone;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер телефону не може бути порожнім.");
            _phone = value.Trim();
        }
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
                throw new ArgumentException("Некоректна адреса електронної пошти.");
            _email = value.Trim().ToLowerInvariant();
        }
    }

    public string PasswordHash { get; set; } = string.Empty;

    // ── Фінанси та знижка ───────────────────────────────────────────────────

    public decimal TotalSpent { get; private set; }

    private decimal _discountPercentage;
    public decimal DiscountPercentage
    {
        get => _discountPercentage;
        private set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Знижка повинна бути від 0 до 100%.");
            _discountPercentage = value;
        }
    }

    // ── Конструктори ────────────────────────────────────────────────────────

    public Customer() { }

    public Customer(string name, string passwordHash, string email, string phone)
    {
        Name = name;
        PasswordHash = passwordHash;
        Email = email;
        Phone = phone;
    }

    // ── Методи ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Встановлює знижку клієнту (0–100%).
    /// </summary>
    public void ApplyDiscount(decimal percentage) => DiscountPercentage = percentage;

    /// <summary>
    /// Додає витрати клієнта і автоматично перераховує накопичувальну знижку.
    /// </summary>
    public void AddExpense(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сума витрат повинна бути більшою за нуль.");

        TotalSpent += amount;
        RecalculateLoyaltyDiscount();
    }

    /// <summary>
    /// Накопичувальна програма лояльності:
    /// від ₴5 000 → 3%, від ₴15 000 → 5%, від ₴30 000 → 7%, від ₴60 000 → 10%.
    /// </summary>
    private void RecalculateLoyaltyDiscount()
    {
        DiscountPercentage = TotalSpent switch
        {
            >= 60_000 => 10,
            >= 30_000 => 7,
            >= 15_000 => 5,
            >= 5_000 => 3,
            _ => 0
        };
    }

    public override string ToString() => $"{Name} ({Email}) · знижка {DiscountPercentage}%";
}
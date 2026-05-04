using System;

namespace Radius.Models;

/// <summary>
/// Співробітник шинного центру Radius.
/// </summary>
public class Employee : IEmployee
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
                throw new ArgumentException("Ім'я співробітника не може бути порожнім.");
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

    private string _position = string.Empty;
    public string Position
    {
        get => _position;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Посада не може бути порожньою.");
            _position = value.Trim();
        }
    }

    private int _experienceYears;
    public int ExperienceYears
    {
        get => _experienceYears;
        set
        {
            if (value < 0)
                throw new ArgumentException("Стаж не може бути від'ємним.");
            _experienceYears = value;
        }
    }

    // ── Стан і рейтинг ──────────────────────────────────────────────────────

    public bool IsAvailable { get; private set; } = true;

    private double _rating = 5.0;
    public double Rating
    {
        get => _rating;
        private set
        {
            if (value < 1.0 || value > 5.0)
                throw new ArgumentOutOfRangeException(nameof(value), "Рейтинг повинен бути від 1.0 до 5.0.");
            _rating = Math.Round(value, 1);
        }
    }

    // ── Конструктори ────────────────────────────────────────────────────────

    public Employee() { }

    public Employee(string name, string phone, string position, int experienceYears)
    {
        Name = name;
        Phone = phone;
        Position = position;
        ExperienceYears = experienceYears;
    }

    // ── Методи ──────────────────────────────────────────────────────────────

    public void SetAvailability(bool isAvailable) => IsAvailable = isAvailable;

    /// <summary>
    /// Оновлює рейтинг як середнє між поточним і новим значенням.
    /// </summary>
    public void UpdateRating(double newRating)
    {
        if (newRating < 1.0 || newRating > 5.0)
            throw new ArgumentOutOfRangeException(nameof(newRating), "Оцінка повинна бути від 1.0 до 5.0.");

        Rating = (_rating + newRating) / 2.0;
    }

    public override string ToString() => $"{Name} · {Position} · ★{Rating:F1}";
}
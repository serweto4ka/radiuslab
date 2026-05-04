using System;

namespace Radius.Models;

/// <summary>
/// Послуга шинного центру (шиномонтаж, балансування, зберігання тощо).
/// </summary>
public class Service
{
    public Guid Id { get; init; } = Guid.NewGuid();

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва послуги не може бути порожньою.");
            _name = value.Trim();
        }
    }

    public string Description { get; set; } = string.Empty;

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Ціна не може бути від'ємною.");
            _price = value;
        }
    }

    private int _durationMinutes;
    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Тривалість послуги повинна бути більшою за нуль.");
            _durationMinutes = value;
        }
    }

    public bool IsActive { get; set; } = true;

    // ── Конструктори ────────────────────────────────────────────────────────

    public Service() { }

    public Service(string name, decimal price, int durationMinutes, string description = "")
    {
        Name = name;
        Price = price;
        DurationMinutes = durationMinutes;
        Description = description;
    }

    public override string ToString() => $"{Name} · ₴{Price} · {DurationMinutes} хв";
}
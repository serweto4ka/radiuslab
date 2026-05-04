using System;

namespace Radius.Models;

/// <summary>
/// Шина в каталозі магазину Radius.
/// </summary>
public class Tire
{
    public Guid Id { get; init; } = Guid.NewGuid();

    private string _brand = string.Empty;
    public string Brand
    {
        get => _brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Бренд не може бути порожнім.");
            _brand = value.Trim();
        }
    }

    public string Model { get; set; } = string.Empty;

    private int _width;
    public int Width
    {
        get => _width;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Ширина повинна бути більшою за нуль.");
            _width = value;
        }
    }

    private int _profile;
    public int Profile
    {
        get => _profile;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Профіль повинен бути більшим за нуль.");
            _profile = value;
        }
    }

    private int _radius;
    public int Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Радіус повинен бути більшим за нуль.");
            _radius = value;
        }
    }

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

    public int StockQuantity { get; set; }
    public TireSeason Season { get; set; } = TireSeason.AllSeason;

    // ── Конструктори ────────────────────────────────────────────────────────

    public Tire() { }

    public Tire(string brand, string model, int width, int profile, int radius, decimal price, TireSeason season)
    {
        Brand = brand;
        Model = model;
        Width = width;
        Profile = profile;
        Radius = radius;
        Price = price;
        Season = season;
    }

    /// <summary>Повертає стандартний розмір шини, наприклад "225/45 R17".</summary>
    public string SizeLabel => $"{Width}/{Profile} R{Radius}";

    public override string ToString() => $"{Brand} {Model} {SizeLabel} · ₴{Price}";
}

public enum TireSeason
{
    Summer,    // Літня
    Winter,    // Зимова
    AllSeason  // Всесезонна
}
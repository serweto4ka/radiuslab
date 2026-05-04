using System;

namespace Radius.Models;

/// <summary>
/// Диск у каталозі магазину Radius.
/// </summary>
public class Disk
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

    private int _radius;
    public int Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Радіус диска повинен бути більшим за нуль.");
            _radius = value;
        }
    }

    private string _material = string.Empty;
    public string Material
    {
        get => _material;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Матеріал не може бути порожнім.");
            _material = value.Trim();
        }
    }

    private string _boltPattern = string.Empty;
    /// <summary>Розболтовка, наприклад "5×114.3".</summary>
    public string BoltPattern
    {
        get => _boltPattern;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Розболтовка не може бути порожньою.");
            _boltPattern = value.Trim();
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
    public string Color { get; set; } = string.Empty;

    // ── Конструктори ────────────────────────────────────────────────────────

    public Disk() { }

    public Disk(string brand, string model, int radius, string material, string boltPattern, decimal price)
    {
        Brand = brand;
        Model = model;
        Radius = radius;
        Material = material;
        BoltPattern = boltPattern;
        Price = price;
    }

    public override string ToString() => $"{Brand} {Model} R{Radius} · {BoltPattern} · ₴{Price}";
}
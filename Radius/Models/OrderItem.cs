using System;

namespace Radius.Models;

/// <summary>
/// Рядок замовлення: один товар або послуга з кількістю та ціною.
/// </summary>
public class OrderItem
{
    public Guid Id { get; init; } = Guid.NewGuid();

    private string _productName = string.Empty;
    public string ProductName
    {
        get => _productName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва товару не може бути порожньою.");
            _productName = value.Trim();
        }
    }

    private decimal _unitPrice;
    public decimal UnitPrice
    {
        get => _unitPrice;
        set
        {
            if (value < 0)
                throw new ArgumentException("Ціна одиниці не може бути від'ємною.");
            _unitPrice = value;
        }
    }

    private int _quantity;
    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Кількість повинна бути більшою за нуль.");
            _quantity = value;
        }
    }

    /// <summary>Загальна вартість рядка (ціна × кількість).</summary>
    public decimal TotalPrice => UnitPrice * Quantity;

    // ── Конструктори ────────────────────────────────────────────────────────

    public OrderItem() { }

    public OrderItem(string productName, decimal unitPrice, int quantity)
    {
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public override string ToString() => $"{ProductName} × {Quantity} · ₴{TotalPrice}";
}
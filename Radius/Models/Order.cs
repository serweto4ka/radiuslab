using System;
using System.Collections.Generic;
using System.Linq;

namespace Radius.Models;

public enum OrderStatus
{
    Pending,    // Обробляється
    Confirmed,  // Підтверджено
    Shipped,    // Відправлено
    Delivered,  // Доставлено
    Cancelled   // Скасовано
}

/// <summary>
/// Замовлення товарів у магазині Radius.
/// </summary>
public class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public Customer Buyer { get; private set; } = null!;

    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public string DeliveryAddress { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    /// <summary>Загальна сума замовлення.</summary>
    public decimal TotalAmount => _items.Sum(i => i.TotalPrice);

    /// <summary>Загальна сума зі знижкою клієнта.</summary>
    public decimal TotalAmountWithDiscount =>
        TotalAmount * (1 - Buyer.DiscountPercentage / 100m);

    // ── Конструктори ────────────────────────────────────────────────────────

    public Order() { }

    public Order(Customer buyer, string deliveryAddress = "")
    {
        Buyer = buyer ?? throw new ArgumentNullException(nameof(buyer));
        DeliveryAddress = deliveryAddress;
    }

    // ── Методи ──────────────────────────────────────────────────────────────

    public void AddItem(OrderItem item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item), "Позиція замовлення не може бути null.");
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Не можна змінювати підтверджене замовлення.");

        _items.Add(item);
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId)
            ?? throw new InvalidOperationException("Позиція не знайдена у замовленні.");
        _items.Remove(item);
    }

    public void Confirm() => ChangeStatus(OrderStatus.Confirmed, OrderStatus.Pending);
    public void Ship() => ChangeStatus(OrderStatus.Shipped, OrderStatus.Confirmed);
    public void Deliver()
    {
        ChangeStatus(OrderStatus.Delivered, OrderStatus.Shipped);
        Buyer.AddExpense(TotalAmountWithDiscount);
    }
    public void Cancel()
    {
        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Неможливо скасувати доставлене замовлення.");
        Status = OrderStatus.Cancelled;
    }

    private void ChangeStatus(OrderStatus next, OrderStatus required)
    {
        if (Status != required)
            throw new InvalidOperationException($"Для переходу в '{next}' потрібен статус '{required}', а не '{Status}'.");
        Status = next;
    }

    public override string ToString() => $"Замовлення {Id.ToString()[..8]} · {Status} · ₴{TotalAmount}";
}
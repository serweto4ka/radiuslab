using System;

namespace Radius.Models;

/// <summary>
/// Інтерфейс клієнта: доступ до email, витрат і знижок.
/// </summary>
public interface ICustomer : IUser
{
    string Email { get; }
    decimal TotalSpent { get; }
    decimal DiscountPercentage { get; }

    void ApplyDiscount(decimal percentage);
    void AddExpense(decimal amount);
}
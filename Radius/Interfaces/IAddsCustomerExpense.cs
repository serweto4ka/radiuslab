using System;

namespace Radius.Interfaces;

public interface IAddsCustomerExpense
{
    void AddExpense(Guid customerId, decimal amount);
}
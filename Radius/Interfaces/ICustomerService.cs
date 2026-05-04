using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface ICustomerService
{
    void RegisterCustomer(Customer customer);
    Customer? GetCustomerById(Guid customerId);
    Customer? GetCustomerByEmail(string email);
    void ChangeCustomerName(Guid customerId, string newName);
    void ChangeCustomerPhone(Guid customerId, string newPhone);
    void ChangeCustomerEmail(Guid customerId, string newEmail);
    void ChangeCustomerPassword(Guid customerId, string newPassword);
    void AddExpense(Guid customerId, decimal amount);
}

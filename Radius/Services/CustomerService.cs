using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Radius.Interfaces;
using Radius.Models;

namespace Radius.Services;

public class CustomerService :
    ICustomerService
{
    private const string DebugLogPath = @"C:\Users\maksi\Desktop\Radius3-master-master\debug-00c82d.log";
    private readonly List<Customer> _customers = new();

    private static void WriteDebugLog(string runId, string hypothesisId, string location, string message, object data)
    {
        var payload = new
        {
            sessionId = "00c82d",
            runId,
            hypothesisId,
            location,
            message,
            data,
            timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
        try
        {
            File.AppendAllText(DebugLogPath, JsonSerializer.Serialize(payload) + Environment.NewLine);
        }
        catch
        {
        }
    }

    public void RegisterCustomer(Customer customer)
    {
        if (customer != null) _customers.Add(customer);
    }

    public Customer GetCustomerById(Guid customerId)
    {
        return _customers.FirstOrDefault(c => c.Id == customerId);
    }

    public Customer? GetCustomerByEmail(string email)
    {
        return _customers.FirstOrDefault(c =>
            c.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    public void ChangeCustomerName(Guid customerId, string newName)
    {
        var customer = GetCustomerById(customerId);
        if (customer != null) customer.Name = newName;
    }

    public void ChangeCustomerPhone(Guid customerId, string newPhone)
    {
        var customer = GetCustomerById(customerId);
        if (customer != null) customer.Phone = newPhone;
    }

    public void ChangeCustomerEmail(Guid customerId, string newEmail)
    {
        var customer = GetCustomerById(customerId);
        if (customer != null) customer.Email = newEmail;
    }

    public void ChangeCustomerPassword(Guid customerId, string newPassword)
    {
        var customer = GetCustomerById(customerId);
        // #region agent log
        WriteDebugLog(
            "initial-debug",
            "H2",
            "CustomerService.cs:ChangeCustomerPassword",
            "ChangeCustomerPassword called",
            new { customerId, hasCustomer = customer != null, passwordLength = newPassword?.Length ?? 0 });
        // #endregion
        if (customer != null) customer.PasswordHash = newPassword;
    }

    public void AddExpense(Guid customerId, decimal amount)
    {
        var customer = GetCustomerById(customerId);
        customer?.AddExpense(amount);
    }
}
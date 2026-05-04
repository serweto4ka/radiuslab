using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsCustomerById
{
    Customer GetCustomerById(Guid customerId);
}
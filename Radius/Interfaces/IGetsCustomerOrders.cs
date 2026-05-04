using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsCustomerOrders
{
    IEnumerable<Order> GetCustomerOrders(Guid customerId);
}
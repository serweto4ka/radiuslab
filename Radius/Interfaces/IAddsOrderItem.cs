using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IAddsOrderItem
{
    void AddOrderItem(Guid orderId, OrderItem item);
}
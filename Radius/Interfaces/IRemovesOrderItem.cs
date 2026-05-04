using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IRemovesOrderItem
{
    void RemoveOrderItem(Guid orderId, OrderItem item);
}
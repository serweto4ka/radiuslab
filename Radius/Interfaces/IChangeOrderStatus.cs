using System;

namespace Radius.Interfaces;

public interface IChangeOrderStatus
{
    void ChangeOrderStatus(Guid orderId, string newStatus);
}
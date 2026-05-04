using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsOrderById
{
    Order GetOrderById(Guid orderId);
}
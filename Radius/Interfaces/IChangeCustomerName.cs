using System;

namespace Radius.Interfaces;

public interface IChangeCustomerName
{
    void ChangeCustomerName(Guid customerId, string newName);
}
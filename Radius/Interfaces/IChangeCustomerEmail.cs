using System;

namespace Radius.Interfaces;

public interface IChangeCustomerEmail
{
    void ChangeCustomerEmail(Guid customerId, string newEmail);
}
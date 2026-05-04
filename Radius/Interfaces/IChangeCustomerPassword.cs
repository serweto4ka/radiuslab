using System;

namespace Radius.Interfaces;

public interface IChangeCustomerPassword
{
    void ChangeCustomerPassword(Guid customerId, string newPassword);
}
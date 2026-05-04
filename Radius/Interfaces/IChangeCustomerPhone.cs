using System;

namespace Radius.Interfaces;

public interface IChangeCustomerPhone
{
    void ChangeCustomerPhone(Guid customerId, string newPhone);
}
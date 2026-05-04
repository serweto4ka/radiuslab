using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsCustomerAppointments
{
    IEnumerable<Appointment> GetCustomerAppointments(Guid customerId);
}
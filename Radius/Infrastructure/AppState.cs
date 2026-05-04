using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Infrastructure;

public class AppState
{
    public Guid? SelectedAppointmentId { get; set; }
    public Customer? CurrentCustomer { get; set; }
    public List<Vehicle> CurrentCustomerVehicles { get; } = new();

    public bool IsAuthenticated => CurrentCustomer != null;
}

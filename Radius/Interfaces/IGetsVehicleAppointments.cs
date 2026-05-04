using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsVehicleAppointments
{
    IEnumerable<Appointment> GetVehicleAppointments(Guid vehicleId);
}
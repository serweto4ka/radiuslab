using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IChangeAppointmentVehicle
{
    void ChangeAppointmentVehicle(Guid appointmentId, Vehicle newVehicle);
}
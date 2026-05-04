using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IChangeAppointmentStatus
{
    void ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus newStatus);
}
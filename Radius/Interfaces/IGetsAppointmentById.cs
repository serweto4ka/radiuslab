using System;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsAppointmentById
{
    Appointment GetAppointmentById(Guid appointmentId);
}
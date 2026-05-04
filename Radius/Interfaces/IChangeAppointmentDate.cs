using System;

namespace Radius.Interfaces;

public interface IChangeAppointmentDate
{
    void ChangeAppointmentDate(Guid appointmentId, DateTime newDate);
}
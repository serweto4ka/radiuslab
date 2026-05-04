using System;

namespace Radius.Interfaces;

public interface IChangeAppointmentDescription
{
    void ChangeAppointmentDescription(Guid appointmentId, string newDescription);
}
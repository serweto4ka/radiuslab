using System;

namespace Radius.Interfaces;

public interface ICancelsAppointment
{
    void CancelAppointment(Guid appointmentId);
}
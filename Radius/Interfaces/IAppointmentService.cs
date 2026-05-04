using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IAppointmentService
{
    void ScheduleAppointment(Appointment appointment);
    Appointment? GetAppointmentById(Guid appointmentId);
    IEnumerable<Appointment> GetCustomerAppointments(Guid customerId);
    IEnumerable<Appointment> GetAppointmentsByDate(DateTime date);
    void ChangeAppointmentDate(Guid appointmentId, DateTime newDate);
    void ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus newStatus);
    void ChangeAppointmentDescription(Guid appointmentId, string newDescription);
    void ChangeAppointmentVehicle(Guid appointmentId, Vehicle newVehicle);
    void CancelAppointment(Guid appointmentId);
    IEnumerable<Appointment> GetVehicleAppointments(Guid vehicleId);
}

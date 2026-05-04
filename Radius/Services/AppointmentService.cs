using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Radius.Interfaces;
using Radius.Models;

namespace Radius.Services;

public class AppointmentService :
    ISchedulesAppointment,
    IGetsAppointmentById,
    IGetsCustomerAppointments,
    IGetsAppointmentsByDate,
    IChangeAppointmentDate,
    IChangeAppointmentStatus,
    IChangeAppointmentDescription,
    IChangeAppointmentVehicle,
    ICancelsAppointment,
    IGetsVehicleAppointments
{
    private const string DebugLogPath = @"C:\Users\maksi\Desktop\Radius3-master-master\debug-00c82d.log";
    private readonly List<Appointment> _appointments = new();

    private static void WriteDebugLog(string runId, string hypothesisId, string location, string message, object data)
    {
        var payload = new
        {
            sessionId = "00c82d",
            runId,
            hypothesisId,
            location,
            message,
            data,
            timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
        try
        {
            File.AppendAllText(DebugLogPath, JsonSerializer.Serialize(payload) + Environment.NewLine);
        }
        catch
        {
        }
    }

    public void ScheduleAppointment(Appointment appointment)
    {
        if (appointment != null) _appointments.Add(appointment);
    }

    public Appointment GetAppointmentById(Guid appointmentId)
    {
        return _appointments.FirstOrDefault(a => a.Id == appointmentId);
    }

    public IEnumerable<Appointment> GetCustomerAppointments(Guid customerId)
    {
        return _appointments.Where(a => a.Client != null && a.Client.Id == customerId);
    }

    public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date)
    {
        return _appointments.Where(a => a.Date.Date == date.Date);
    }

    public void ChangeAppointmentDate(Guid appointmentId, DateTime newDate)
    {
        var appointment = GetAppointmentById(appointmentId);
        if (appointment != null) appointment.Date = newDate;
    }

    public void ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus newStatus)
    {
        var appointment = GetAppointmentById(appointmentId);
        // #region agent log
        WriteDebugLog(
            "initial-debug",
            "H1",
            "AppointmentService.cs:ChangeAppointmentStatus",
            "ChangeAppointmentStatus called",
            new { appointmentId, hasAppointment = appointment != null, requestedStatus = newStatus.ToString() });
        // #endregion
        if (appointment == null) return;

        switch (newStatus)
        {
            case AppointmentStatus.InProgress:
                appointment.Start();
                break;
            case AppointmentStatus.Completed:
                appointment.Complete(appointment.TotalCost);
                break;
            case AppointmentStatus.Cancelled:
                appointment.Cancel();
                break;
            case AppointmentStatus.Scheduled:
            default:
                break;
        }
    }

    public void ChangeAppointmentDescription(Guid appointmentId, string newDescription)
    {
        var appointment = GetAppointmentById(appointmentId);
        if (appointment != null) appointment.Description = newDescription;
    }

    public void ChangeAppointmentVehicle(Guid appointmentId, Vehicle newVehicle)
    {
        var appointment = GetAppointmentById(appointmentId);
        if (appointment != null) appointment.Car = newVehicle;
    }

    public void CancelAppointment(Guid appointmentId)
    {
        ChangeAppointmentStatus(appointmentId, AppointmentStatus.Cancelled);
    }

    public IEnumerable<Appointment> GetVehicleAppointments(Guid vehicleId)
    {
        return _appointments.Where(a => a.Car != null && a.Car.Id == vehicleId);
    }
}
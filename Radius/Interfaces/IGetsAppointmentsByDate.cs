using System;
using System.Collections.Generic;
using Radius.Models;

namespace Radius.Interfaces;

public interface IGetsAppointmentsByDate
{
    IEnumerable<Appointment> GetAppointmentsByDate(DateTime date);
}
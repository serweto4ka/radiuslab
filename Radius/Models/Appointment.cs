using System;

namespace Radius.Models;

public enum AppointmentStatus
{
    Scheduled,   // Заплановано
    InProgress,  // Виконується
    Completed,   // Виконано
    Cancelled    // Скасовано
}

/// <summary>
/// Запис клієнта до шинного центру Radius.
/// </summary>
public class Appointment
{
    public Guid Id { get; init; } = Guid.NewGuid();

    // ── Учасники ────────────────────────────────────────────────────────────

    public Customer Client { get; set; } = null!;
    public Vehicle Car { get; set; } = null!;
    public Employee? AssignedEmployee { get; set; }

    // ── Час і статус ────────────────────────────────────────────────────────

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set
        {
            if (value < DateTime.UtcNow.AddMinutes(-5))
                throw new ArgumentException("Не можна створити запис на час у минулому.");
            _date = value;
        }
    }

    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;

    // ── Деталі ──────────────────────────────────────────────────────────────

    public string Description { get; set; } = string.Empty;

    /// <summary>Загальна вартість запису на основі виконаних послуг.</summary>
    public decimal TotalCost { get; private set; }

    // ── Конструктори ────────────────────────────────────────────────────────

    public Appointment() { }

    public Appointment(Customer client, Vehicle car, DateTime date, string description, Employee? employee = null)
    {
        Client = client;
        Car = car;
        Date = date;
        Description = description;
        AssignedEmployee = employee;
    }

    // ── Методи ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Переводить запис у статус "Виконується". Можливо лише зі стану Scheduled.
    /// </summary>
    public void Start()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException($"Неможливо розпочати запис зі статусом '{Status}'.");

        Status = AppointmentStatus.InProgress;
        AssignedEmployee?.SetAvailability(false);
    }

    /// <summary>
    /// Завершує запис із вказаною вартістю. Можливо лише зі стану InProgress.
    /// </summary>
    public void Complete(decimal cost)
    {
        if (Status != AppointmentStatus.InProgress)
            throw new InvalidOperationException($"Неможливо завершити запис зі статусом '{Status}'.");
        if (cost < 0)
            throw new ArgumentException("Вартість не може бути від'ємною.");

        Status = AppointmentStatus.Completed;
        TotalCost = cost;

        Client.AddExpense(cost);
        AssignedEmployee?.SetAvailability(true);
    }

    /// <summary>
    /// Скасовує запис. Можливо лише зі стану Scheduled.
    /// </summary>
    public void Cancel()
    {
        if (Status != AppointmentStatus.Scheduled)
            throw new InvalidOperationException($"Неможливо скасувати запис зі статусом '{Status}'.");

        Status = AppointmentStatus.Cancelled;
    }

    public override string ToString() =>
        $"{Car} · {Date:dd.MM.yyyy HH:mm} · {Status}";
}
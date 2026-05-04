using System;

namespace Radius.Models;

/// <summary>
/// Відгук клієнта про відвідування шинного центру Radius.
/// </summary>
public class Review
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public Customer Author { get; private set; } = null!;
    public Employee? ReviewedEmployee { get; private set; }
    public Appointment? RelatedAppointment { get; private set; }

    private int _rating;
    /// <summary>Оцінка від 1 до 5.</summary>
    public int Rating
    {
        get => _rating;
        set
        {
            if (value < 1 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value), "Оцінка повинна бути від 1 до 5.");
            _rating = value;
        }
    }

    private string _comment = string.Empty;
    public string Comment
    {
        get => _comment;
        set => _comment = value?.Trim() ?? string.Empty;
    }

    public bool IsPublished { get; private set; } = false;

    // ── Конструктори ────────────────────────────────────────────────────────

    public Review() { }

    public Review(Customer author, int rating, string comment,
                  Employee? reviewedEmployee = null,
                  Appointment? relatedAppointment = null)
    {
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Rating = rating;
        Comment = comment;
        ReviewedEmployee = reviewedEmployee;
        RelatedAppointment = relatedAppointment;
    }

    // ── Методи ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Публікує відгук і оновлює рейтинг співробітника (якщо вказаний).
    /// </summary>
    public void Publish()
    {
        if (IsPublished)
            throw new InvalidOperationException("Відгук вже опубліковано.");

        IsPublished = true;
        ReviewedEmployee?.UpdateRating(Rating);
    }

    public override string ToString() =>
        $"★{Rating} · {Author.Name} · {CreatedAt:dd.MM.yyyy}";
}
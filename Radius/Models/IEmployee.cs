using System;

namespace Radius.Models;

/// <summary>
/// Інтерфейс співробітника: посада, рейтинг і доступність.
/// </summary>
public interface IEmployee : IUser
{
    string Position { get; }
    int ExperienceYears { get; }
    double Rating { get; }
    bool IsAvailable { get; }

    void SetAvailability(bool isAvailable);
    void UpdateRating(double newRating);
}
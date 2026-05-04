using System;

namespace Radius.Models;

/// <summary>
/// Базовий інтерфейс для всіх користувачів системи (клієнт, співробітник).
/// </summary>
public interface IUser
{
    Guid Id { get; }
    string Name { get; }
    string Phone { get; }
    DateTime RegistrationDate { get; }
}
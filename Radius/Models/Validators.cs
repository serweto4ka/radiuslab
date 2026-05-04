using System.Text.RegularExpressions;

namespace Radius.ViewModels;

/// <summary>
/// Статичні методи валідації, що використовуються в усіх ViewModel.
/// Кожен метод повертає null якщо дані коректні, або текст помилки.
/// </summary>
public static class Validators
{
    // Скомпіловані regex — ефективніші за звичайні при частому виклику
    private static readonly Regex PhoneRegex = new(@"^\+380\d{9}$", RegexOptions.Compiled);
    private static readonly Regex EmailRegex = new(@"^[\w\-.]+@([\w-]+\.)+[\w-]{2,}$", RegexOptions.Compiled);
    private static readonly Regex PasswordRegex = new(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", RegexOptions.Compiled);
    private static readonly Regex LicensePlateRegex = new(@"^[A-Z]{2}\d{4}[A-Z]{2}$", RegexOptions.Compiled);
    private static readonly Regex VinRegex = new(@"^[A-HJ-NPR-Z0-9]{17}$", RegexOptions.Compiled);

    public static string? ValidatePhone(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return "Номер телефону не може бути порожнім.";
        if (!PhoneRegex.IsMatch(phone.Trim()))
            return "Невірний формат телефону. Очікується +380XXXXXXXXX.";
        return null;
    }

    public static string? ValidateEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return "Адреса електронної пошти не може бути порожньою.";
        if (!EmailRegex.IsMatch(email.Trim()))
            return "Невірний формат електронної пошти.";
        return null;
    }

    public static string? ValidatePassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return "Пароль не може бути порожнім.";
        if (!PasswordRegex.IsMatch(password))
            return "Пароль повинен містити мінімум 8 символів, одну велику літеру, цифру і спецсимвол (@$!%*?&).";
        return null;
    }

    public static string? ValidateLicensePlate(string? plate)
    {
        if (string.IsNullOrWhiteSpace(plate))
            return "Номерний знак не може бути порожнім.";
        if (!LicensePlateRegex.IsMatch(plate.Trim().ToUpperInvariant()))
            return "Невірний формат номерного знаку. Очікується AA1234BB.";
        return null;
    }

    public static string? ValidateVin(string? vin)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return "VIN-код не може бути порожнім.";
        if (vin.Trim().Length != 17 || !VinRegex.IsMatch(vin.Trim().ToUpperInvariant()))
            return "VIN-код повинен містити рівно 17 символів (літери A-Z без I,O,Q та цифри).";
        return null;
    }

    public static string? ValidateNotEmpty(string? value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return $"Поле '{fieldName}' не може бути порожнім.";
        return null;
    }

    public static string? ValidateYear(int year)
    {
        int current = DateTime.UtcNow.Year;
        if (year < 1900 || year > current + 1)
            return $"Рік випуску повинен бути між 1900 і {current + 1}.";
        return null;
    }

    public static string? ValidatePositiveDecimal(decimal value, string fieldName)
    {
        if (value <= 0)
            return $"'{fieldName}' повинна бути більшою за нуль.";
        return null;
    }

    public static string? ValidatePositiveInt(int value, string fieldName)
    {
        if (value <= 0)
            return $"'{fieldName}' повинно бути більшим за нуль.";
        return null;
    }
}
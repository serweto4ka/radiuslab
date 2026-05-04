using System;

namespace Radius.Models;

/// <summary>
/// Сезонне зберігання шин клієнта у шинному центрі Radius.
/// </summary>
public class SeasonalStorage
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public Customer Client { get; private set; } = null!;

    private string _tireDescription = string.Empty;
    public string TireDescription
    {
        get => _tireDescription;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Опис шин не може бути порожнім.");
            _tireDescription = value.Trim();
        }
    }

    private int _tireQuantity;
    public int TireQuantity
    {
        get => _tireQuantity;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Кількість шин повинна бути більшою за нуль.");
            _tireQuantity = value;
        }
    }

    private DateTime _startDate;
    public DateTime StartDate
    {
        get => _startDate;
        private set
        {
            if (value > DateTime.UtcNow.AddMinutes(1))
                throw new ArgumentException("Дата початку зберігання не може бути в майбутньому.");
            _startDate = value;
        }
    }

    private DateTime _expirationDate;
    public DateTime ExpirationDate
    {
        get => _expirationDate;
        set
        {
            if (value <= _startDate)
                throw new ArgumentException("Дата закінчення повинна бути пізніше дати початку.");
            _expirationDate = value;
        }
    }

    /// <summary>Зберігання вважається активним, поки не минув термін.</summary>
    public bool IsActive => DateTime.UtcNow <= ExpirationDate;

    /// <summary>Відсоток використаного терміну зберігання (0–100).</summary>
    public double StorageUsedPercent
    {
        get
        {
            double total = (ExpirationDate - StartDate).TotalDays;
            double used = (DateTime.UtcNow - StartDate).TotalDays;
            return total <= 0 ? 100 : Math.Clamp(used / total * 100, 0, 100);
        }
    }

    public string Notes { get; set; } = string.Empty;

    // ── Конструктори ────────────────────────────────────────────────────────

    public SeasonalStorage() { }

    public SeasonalStorage(Customer client, string tireDescription, int tireQuantity,
                           DateTime startDate, DateTime expirationDate)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        TireDescription = tireDescription;
        TireQuantity = tireQuantity;
        StartDate = startDate;
        ExpirationDate = expirationDate;
    }

    public override string ToString() =>
        $"{Client.Name} · {TireDescription} ({TireQuantity} шт) · до {ExpirationDate:dd.MM.yyyy}";
}
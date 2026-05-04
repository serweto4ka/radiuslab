using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Radius.ViewModels;

/// <summary>
/// Базовий ViewModel для всіх екранів додатку.
/// Реалізує INotifyPropertyChanged і надає зручний SetProperty.
/// </summary>
public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Оновлює поле і сповіщає UI, якщо значення змінилось.
    /// Повертає true, якщо зміна відбулась.
    /// </summary>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    // ── Спільні стани UI ────────────────────────────────────────────────────

    private bool _isBusy;
    /// <summary>True поки виконується async-операція (блокує повторне натискання).</summary>
    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    private bool _isEmpty;
    /// <summary>True коли колекція порожня (для відображення заглушки).</summary>
    public bool IsEmpty
    {
        get => _isEmpty;
        set => SetProperty(ref _isEmpty, value);
    }

    private string _errorMessage = string.Empty;
    /// <summary>Текст помилки для відображення в UI. Порожній рядок = немає помилки.</summary>
    public string ErrorMessage
    {
        get => _errorMessage;
        set => SetProperty(ref _errorMessage, value);
    }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    // ── Зручні методи ──────────────────────────────────────────────────────

    /// <summary>Встановлює помилку і сповіщає HasError.</summary>
    protected void SetError(string message)
    {
        ErrorMessage = message;
        OnPropertyChanged(nameof(HasError));
    }

    /// <summary>Очищає помилку.</summary>
    protected void ClearError()
    {
        ErrorMessage = string.Empty;
        OnPropertyChanged(nameof(HasError));
    }
}
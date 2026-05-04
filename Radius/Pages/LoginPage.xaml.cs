using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Models;
using Radius.Services;

namespace Radius.Pages;

public partial class LoginPage : ContentPage
{
    private readonly CustomerService _customerService;
    private readonly AppState _appState;

    public LoginPage()
    {
        InitializeComponent();
        var services = ServiceRegistry.Services ?? throw new InvalidOperationException("DI container is not initialized.");
        _customerService = services.GetRequiredService<CustomerService>();
        _appState = services.GetRequiredService<AppState>();
    }

    private async void OnSignInClicked(object? sender, EventArgs e)
    {
        await AuthenticateAsync(registerIfNotFound: false);
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        await AuthenticateAsync(registerIfNotFound: true);
    }

    private async Task AuthenticateAsync(bool registerIfNotFound)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlertAsync("Помилка", "Заповніть усі поля.", "OK");
                return;
            }

            Customer? customer = _customerService.GetCustomerByEmail(EmailEntry.Text.Trim());

            if (customer == null && registerIfNotFound)
            {
                customer = new Customer(
                    NameEntry.Text.Trim(),
                    PasswordEntry.Text.Trim(),
                    EmailEntry.Text.Trim(),
                    PhoneEntry.Text.Trim());
                _customerService.RegisterCustomer(customer);
            }

            if (customer == null)
            {
                await DisplayAlertAsync("Користувача не знайдено", "Натисніть 'Зареєструватися' для створення акаунту.", "OK");
                return;
            }

            _appState.CurrentCustomer = customer;
            _appState.CurrentCustomerVehicles.Clear();
            App.OpenMainShell();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Помилка", ex.Message, "OK");
        }
    }
}

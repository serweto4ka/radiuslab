using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Services;

namespace Radius.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly AppState _appState;
    private readonly CustomerService _customerService;

    public ProfilePage()
    {
        InitializeComponent();
        var services = ServiceRegistry.Services ?? throw new InvalidOperationException("DI container is not initialized.");
        _appState = services.GetRequiredService<AppState>();
        _customerService = services.GetRequiredService<CustomerService>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_appState.CurrentCustomer == null)
        {
            return;
        }

        NameEntry.Text = _appState.CurrentCustomer.Name;
        PhoneEntry.Text = _appState.CurrentCustomer.Phone;
        EmailEntry.Text = _appState.CurrentCustomer.Email;
        StatsLabel.Text = $"Авто: {_appState.CurrentCustomerVehicles.Count}";
    }

    private async void OnSaveClicked(object? sender, EventArgs e)
    {
        if (_appState.CurrentCustomer == null)
        {
            return;
        }

        try
        {
            _customerService.ChangeCustomerName(_appState.CurrentCustomer.Id, NameEntry.Text ?? string.Empty);
            _customerService.ChangeCustomerPhone(_appState.CurrentCustomer.Id, PhoneEntry.Text ?? string.Empty);
            _customerService.ChangeCustomerEmail(_appState.CurrentCustomer.Id, EmailEntry.Text ?? string.Empty);
            await DisplayAlertAsync("Готово", "Профіль оновлено.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Помилка", ex.Message, "OK");
        }
    }

    private void OnLogoutClicked(object? sender, EventArgs e)
    {
        _appState.CurrentCustomer = null;
        _appState.CurrentCustomerVehicles.Clear();
        App.Current!.Windows[0].Page = new NavigationPage(new LoginPage());
    }
}

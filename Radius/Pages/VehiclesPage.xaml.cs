using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Models;
using Radius.Services;

namespace Radius.Pages;

public partial class VehiclesPage : ContentPage
{
    private readonly VehicleService _vehicleService;
    private readonly AppState _appState;

    public VehiclesPage()
    {
        InitializeComponent();
        var services = ServiceRegistry.Services ?? throw new InvalidOperationException("DI container is not initialized.");
        _vehicleService = services.GetRequiredService<VehicleService>();
        _appState = services.GetRequiredService<AppState>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshVehicles();
    }

    private async void OnAddVehicleClicked(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(BrandEntry.Text) ||
                string.IsNullOrWhiteSpace(ModelEntry.Text) ||
                string.IsNullOrWhiteSpace(PlateEntry.Text))
            {
                await DisplayAlert("Помилка", "Заповніть марку, модель і номер.", "OK");
                return;
            }

            var vin = Guid.NewGuid().ToString("N")[..17].ToUpperInvariant();
            var vehicle = new Vehicle(
                BrandEntry.Text.Trim(),
                ModelEntry.Text.Trim(),
                PlateEntry.Text.Trim(),
                DateTime.Today.Year,
                vin);

            _vehicleService.AddVehicle(vehicle);
            _appState.CurrentCustomerVehicles.Add(vehicle);
            BrandEntry.Text = string.Empty;
            ModelEntry.Text = string.Empty;
            PlateEntry.Text = string.Empty;
            RefreshVehicles();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", ex.Message, "OK");
        }
    }

    private void RefreshVehicles()
    {
        VehiclesCollection.ItemsSource = _appState.CurrentCustomerVehicles.Select(v => v.ToString()).ToList();
    }
}

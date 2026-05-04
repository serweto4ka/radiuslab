using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Models;
using Radius.Services;

namespace Radius.Pages;

public partial class DashboardPage : ContentPage
{
    private readonly AppointmentService _appointmentService;
    private readonly AppState _appState;

    public DashboardPage()
    {
        InitializeComponent();
        var services = ServiceRegistry.Services ?? throw new InvalidOperationException("DI container is not initialized.");
        _appointmentService = services.GetRequiredService<AppointmentService>();
        _appState = services.GetRequiredService<AppState>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_appState.CurrentCustomer == null)
        {
            AppointmentsCollection.ItemsSource = new List<AppointmentCard>();
            return;
        }

        var todayItems = _appointmentService
            .GetCustomerAppointments(_appState.CurrentCustomer.Id)
            .Where(a => a.Date.Date == DateTime.Today)
            .OrderBy(a => a.Date)
            .Select(a => new AppointmentCard(a))
            .ToList();

        AppointmentsCollection.ItemsSource = todayItems;
    }

    private async void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as AppointmentCard;
        if (selected == null)
        {
            return;
        }

        _appState.SelectedAppointmentId = selected.Id;
        ((CollectionView)sender!).SelectedItem = null;
        await Shell.Current.GoToAsync("AppointmentDetailsPage");
    }

    private sealed record AppointmentCard(Guid Id, string Summary, string StatusText)
    {
        public AppointmentCard(Appointment item)
            : this(
                item.Id,
                $"{item.Date:HH:mm} — {item.Car.Brand} {item.Car.Model} — {item.Description}",
                $"Статус: {item.Status}")
        {
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Models;
using Radius.Services;

namespace Radius.Pages;

public partial class BookAppointmentPage : ContentPage
{
    private readonly AppointmentService _appointmentService;
    private readonly AppState _appState;

    public BookAppointmentPage()
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
            return;
        }

        CustomerInfoLabel.Text = $"Клієнт: {_appState.CurrentCustomer.Name} · {_appState.CurrentCustomer.Phone}";
        VehiclePicker.ItemsSource = _appState.CurrentCustomerVehicles.Select(v => v.ToString()).ToList();
    }

    private async void OnSubmitClicked(object? sender, EventArgs e)
    {
        try
        {
            if (_appState.CurrentCustomer == null ||
                VehiclePicker.SelectedIndex < 0 ||
                ServicePicker.SelectedIndex < 0)
            {
                await DisplayAlertAsync("Помилка", "Спершу додайте авто на сторінці 'Мої машини'.", "OK");
                return;
            }

            var customer = _appState.CurrentCustomer;
            var vehicle = _appState.CurrentCustomerVehicles[VehiclePicker.SelectedIndex];

            var selectedDate = DatePickerControl.Date ?? DateTime.Today;
            var selectedTime = TimePickerControl.Time ?? TimeSpan.Zero;
            var date = selectedDate.Date + selectedTime;
            var selectedService = ServicePicker.Items[ServicePicker.SelectedIndex];
            var description = string.IsNullOrWhiteSpace(CommentEditor.Text)
                ? selectedService
                : $"{selectedService}. {CommentEditor.Text.Trim()}";

            var appointment = new Appointment(customer, vehicle, date, description);
            _appointmentService.ScheduleAppointment(appointment);

            await DisplayAlertAsync("Успіх", "Запис створено.", "OK");
            await Shell.Current.GoToAsync("DashboardPage");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Помилка", ex.Message, "OK");
        }
    }
}

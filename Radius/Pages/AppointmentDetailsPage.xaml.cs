using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Models;
using Radius.Services;

namespace Radius.Pages;

public partial class AppointmentDetailsPage : ContentPage
{
    private readonly AppointmentService _appointmentService;
    private readonly AppState _appState;
    private Appointment? _current;

    public AppointmentDetailsPage()
    {
        InitializeComponent();
        var services = ServiceRegistry.Services ?? throw new InvalidOperationException("DI container is not initialized.");
        _appointmentService = services.GetRequiredService<AppointmentService>();
        _appState = services.GetRequiredService<AppState>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_appState.SelectedAppointmentId is not Guid id)
        {
            return;
        }

        _current = _appointmentService.GetAppointmentById(id);
        if (_current == null)
        {
            return;
        }

        ClientLabel.Text = $"Клієнт: {_current.Client.Name}";
        CarLabel.Text = $"Авто: {_current.Car.Brand} {_current.Car.Model} ({_current.Car.LicensePlate})";
        ServiceLabel.Text = $"Послуга: {_current.Description}";
        DateLabel.Text = $"Дата: {_current.Date:dd.MM.yyyy HH:mm}";
        CostLabel.Text = $"Вартість: {_current.TotalCost:0} грн";
        StatusLabel.Text = $"Статус: {_current.Status}";
        DescriptionEditor.Text = _current.Description;
    }

    private async void OnCancelClicked(object? sender, EventArgs e)
    {
        if (_current == null)
        {
            return;
        }

        try
        {
            _appointmentService.CancelAppointment(_current.Id);
            StatusLabel.Text = $"Статус: {AppointmentStatus.Cancelled}";
            await DisplayAlert("Готово", "Запис скасовано.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", ex.Message, "OK");
        }
    }

    private async void OnCompleteClicked(object? sender, EventArgs e)
    {
        if (_current == null)
        {
            return;
        }

        try
        {
            if (_current.Status == AppointmentStatus.Scheduled)
            {
                _appointmentService.ChangeAppointmentStatus(_current.Id, AppointmentStatus.InProgress);
            }

            _appointmentService.ChangeAppointmentStatus(_current.Id, AppointmentStatus.Completed);
            StatusLabel.Text = $"Статус: {AppointmentStatus.Completed}";
            await DisplayAlert("Готово", "Запис позначено як виконаний.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Помилка", ex.Message, "OK");
        }
    }
}

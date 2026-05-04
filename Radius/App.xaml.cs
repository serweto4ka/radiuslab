using Microsoft.Extensions.DependencyInjection;
using Radius.Infrastructure;
using Radius.Models;
using Radius.Services;

namespace Radius
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SeedDemoData();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Pages.LoginPage()));
        }

        public static void OpenMainShell()
        {
            if (Current?.Windows.Count > 0)
            {
                Current.Windows[0].Page = new AppShell();
            }
        }

        private static void SeedDemoData()
        {
            var services = ServiceRegistry.Services;
            if (services == null)
            {
                return;
            }

            var appointmentService = services.GetRequiredService<AppointmentService>();
            if (appointmentService.GetAppointmentsByDate(DateTime.Today).Any())
            {
                return;
            }

            var customerService = services.GetRequiredService<CustomerService>();
            var vehicleService = services.GetRequiredService<VehicleService>();
            var appState = services.GetRequiredService<AppState>();

            var customer = new Customer("Demo Client", "demo_hash", "demo@radius.app", "+380991112233");
            customerService.RegisterCustomer(customer);

            var vehicle = new Vehicle("BMW", "X5", "AA1234KT", 2021, "WBA11111111111111");
            vehicleService.AddVehicle(vehicle);

            var appointment = new Appointment(
                customer,
                vehicle,
                DateTime.Today.AddHours(11),
                "Заміна шин + балансування");

            appointmentService.ScheduleAppointment(appointment);

            appState.CurrentCustomer = null;
            appState.CurrentCustomerVehicles.Clear();
        }
    }
}
namespace Radius
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Pages.AppointmentDetailsPage), typeof(Pages.AppointmentDetailsPage));
        }
    }
}

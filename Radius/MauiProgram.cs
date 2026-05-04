using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using Radius.Infrastructure;
using Radius.Services;

namespace Radius
{
    public static class MauiProgram
    {
        private const string DebugLogPath = @"C:\Users\maksi\Desktop\Radius3-master-master\debug-00c82d.log";

        private static void WriteDebugLog(string runId, string hypothesisId, string location, string message, object data)
        {
            var payload = new
            {
                sessionId = "00c82d",
                runId,
                hypothesisId,
                location,
                message,
                data,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };
            try
            {
                File.AppendAllText(DebugLogPath, JsonSerializer.Serialize(payload) + Environment.NewLine);
            }
            catch
            {
            }
        }

        public static MauiApp CreateMauiApp()
        {
            // #region agent log
            WriteDebugLog(
                "initial-debug",
                "H6",
                "MauiProgram.cs:CreateMauiApp",
                "CreateMauiApp invoked",
                new { platform = DeviceInfo.Platform.ToString() });
            // #endregion

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<AppState>();
            builder.Services.AddSingleton<AppointmentService>();
            builder.Services.AddSingleton<CustomerService>();
            builder.Services.AddSingleton<VehicleService>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();
            ServiceRegistry.Services = app.Services;
            return app;
        }
    }
}

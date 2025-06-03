using Data_Access_Layer;
using Logic_Layer;
using Logic_Layer.Interface.DAL;
using Logic_Layer.Interface.LL;
using Logic_Layer.Services;
using Logic_Layer.Services.Planes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace Winforms_App
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IUserAccountDAL, UserAccountDAL>();
            services.AddSingleton<IIDDAL, DocumentDAL>();
            services.AddSingleton<IPassportDAL, DocumentDAL>();
            services.AddSingleton<ITicketsDAL, TicketsDAL>();
            services.AddSingleton<IFlightDAL, FlightDAL>();
            services.AddSingleton<IPlaneDAL, PlaneDAL>();
            services.AddSingleton<IAirportDAL, AirportDAL>();

            services.AddSingleton<IUserAccountService, UserAccountService>();
            services.AddSingleton<IPassportService, PassportService>();
            services.AddSingleton<IIDService, IDService>();
            services.AddSingleton<ITicketService, TicketService>();
            services.AddSingleton<IPlaneSeatService, PlaneSeatsService>();
            services.AddSingleton<IPlaneService, PlaneService>();
            services.AddSingleton<IFlightService, FlightService>();
            services.AddSingleton<IAirportService, AirportService>();

            services.AddSingleton<IPlaneSeatsServiceFactory, PlaneSeatsServiceFactory>();

            services.AddTransient<Form1>();
            services.AddTransient<Login>();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                var loginForm = serviceProvider.GetRequiredService<Login>();
                Application.Run(loginForm);
            }
        }
    }
}

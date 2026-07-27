using DevExpress.Xpo.Logger.Transport;
using EKYNOX_HEI.DAPP.Controller;
using EKYNOX_HEI.DAPP.View;
using EKYNOX_HEI.DATA.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Host = Microsoft.Extensions.Hosting.Host;

namespace EKYNOX_HEI.DAPP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {

                    // SQLite bağlantısı
                    services.AddDbContext<DatabaseContext>(options =>
                    {
                        options.UseSqlite("Data Source=EkynoxHei.db");
                    });

                    //// Servisler
                    services.AddScoped<clsInstitutions>();
                    services.AddScoped<clsUsers>();
                    services.AddScoped<clsEducationAttendance>();

                    //// Formlar
                    services.AddTransient<frmInstitutions>();
                    services.AddTransient<frmMain>();
                    services.AddTransient<frmLoading>();
                    services.AddTransient<frmUsers>();
                    services.AddTransient<frmLogin>();
                    services.AddTransient<frmEducationAttendanceList>();
                    services.AddTransient<frmEducationAttendance>();
                    services.AddTransient<frmImageReadConfirm>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                db.Database.Migrate();
            }

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.WXICompact);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(host.Services.GetRequiredService<frmLoading>());
        }
    }
}
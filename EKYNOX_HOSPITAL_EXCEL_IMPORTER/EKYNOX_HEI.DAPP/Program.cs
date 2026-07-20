using DevExpress.Xpo.Logger.Transport;
using EKYNOX_HEI.DAPP.View;
using EKYNOX_HEI.DATA.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                        options.UseSqlite("Data Source=Ekynox.db");
                    });

                    //// Servisler
                    //services.AddScoped<CustomerService>();

                    //// Formlar
                    //services.AddTransient<frmMain>();
                })
                .Build();

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.WXICompact);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}
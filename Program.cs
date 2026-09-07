using WCRCorder.Services;
using WCRCorder.Hosting;

namespace WCRCorder
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        //Точка входа приложения: создаёт ApplicationService, выполняет инициализацию и запускает WinForms message loop.
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var application = new ApplicationService();

            application.Initialize();

            try
            {
                System.Windows.Forms.Application.Run(new TrayApplicationContext(application));
            }
            finally
            {
                application.Shutdown();
            }
        }
    }
}
using JobAnalyzer.BLL;
using Microsoft.Extensions.Configuration;

namespace JobAnalyzer
{
    internal static class Program
    {
        /// <summary>
        /// The AICallSettings loaded from appsettings.json, available application-wide.
        /// </summary>
        public static AICallSettings AICallSettings { get; private set; } = new();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Load configuration from appsettings.json
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            ApplicationConfiguration.Initialize();
            Application.Run(new FrmJobDetail());
        }
    }
}
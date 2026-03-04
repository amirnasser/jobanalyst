using System.Diagnostics;
using Newtonsoft.Json;

namespace JobAnalyzer
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            //if (File.Exists("current_folder.txt"))
            //{
            //    var dir = File.ReadAllText("current_folder.txt");
            //    Application.Run(new FrmJobDetail(dir));
            //}
            Application.Run(new frmMain());
        }
    }
}
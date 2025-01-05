using FileLogger;

namespace Upscaler
{
    internal static class Program
    {
        private static IFileLogger fileLogger;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += ExceptionHandler;

            ApplicationConfiguration.Initialize();
            fileLogger = new FileLogger.FileLogger("RealEsrganUpscalerGUI");
            Application.Run(new MainForm(fileLogger));
            return;

            void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
            {
                var e = (Exception)args.ExceptionObject;
                var error = $"An error occurred\n\n{e}";
                fileLogger.Log(error);
                MessageBox.Show(error);
            }
        }
    }
}
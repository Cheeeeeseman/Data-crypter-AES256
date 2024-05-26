using swBootloader;
using swCrypter;

namespace swLocker
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
            Configurator.LoadSettingsFromJSON(AppConsts.JSON_PATH);
            Application.Run(new Form1());
        }
    }
}
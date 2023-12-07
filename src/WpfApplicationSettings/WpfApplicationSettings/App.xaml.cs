using System.Configuration;
using System.Data;
using System.Windows;
using WpfApplicationSettings.FMSettings;

namespace WpfApplicationSettings
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsPortable { get; private set; } = true;
        public static string BuildInfo { get; private set; } = "Release";
        public const string ApplicationName = "FriskyMouse";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }

}

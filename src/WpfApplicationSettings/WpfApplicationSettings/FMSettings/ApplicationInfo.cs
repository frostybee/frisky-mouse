using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplicationSettings.FMSettings
{
    internal static class ApplicationInfo
    {
        public static bool IsPortable { get; set; } = false;
        public static string BuildInfo { get; set; } = "Release";
        public const string ApplicationName = "FriskyMouse";

    }
}

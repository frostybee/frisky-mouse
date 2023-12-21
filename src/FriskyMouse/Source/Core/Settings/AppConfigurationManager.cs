namespace FriskyMouse.Settings;

internal static class AppConfigurationManager
{
    public static AppConfigurationInfo Current { get; set; }

    static AppConfigurationManager()
    {
        Console.WriteLine("Configuration manager has been loaded/called");
    }
    public static void SayHello()
    {
        Console.WriteLine("Saying hello... ");
    }
}
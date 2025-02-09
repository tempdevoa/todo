namespace Todo.AcceptanceTests
{
    public static class AppiumConfiguration
    {
        public static Uri AppiumServerUri { get; } = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");

        public static string PlatformName { get; } = "Android";

        public static string DeviceName { get; } = "Android Emulator";
    }
}
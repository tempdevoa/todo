using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium;

namespace Todo.AcceptanceTests
{
    public static class AppiumDriverFactory
    {
        public static AndroidDriver CreateAndriodDriver()
        {
            var appiumOptions = new AppiumOptions
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                PlatformName = AppiumConfiguration.PlatformName,
                DeviceName = AppiumConfiguration.DeviceName
            };
            
            appiumOptions.AddAdditionalAppiumOption("noReset", true);
            appiumOptions.AddAdditionalAppiumOption("appium:appPackage", "com.todo.todoapp");
            appiumOptions.AddAdditionalAppiumOption("appium:appWaitActivity", "crc642cfc5ea161b91bf0.MainActivity");

            return new AndroidDriver(AppiumConfiguration.AppiumServerUri, appiumOptions);
        }
    }
}

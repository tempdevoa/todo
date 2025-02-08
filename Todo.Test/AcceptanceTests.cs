using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;

namespace Todo.AcceptanceTests;

public class AcceptanceTests
{
    private AndroidDriver driver;

    [OneTimeSetUp]
    public void SetUp()
    {
        var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");
        
        var appiumOptions = new AppiumOptions
        {
            AutomationName = AutomationName.AndroidUIAutomator2,
            PlatformName = "Android",
            DeviceName = "Android Emulator"
        };

        appiumOptions.AddAdditionalAppiumOption("noReset", true);
        appiumOptions.AddAdditionalAppiumOption("appium:appPackage", "com.todo.todoapp");
        appiumOptions.AddAdditionalAppiumOption("appium:appWaitActivity", "crc642cfc5ea161b91bf0.MainActivity");
        
        driver = new AndroidDriver(serverUri, appiumOptions);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Test]
    public void AppShouldOpenAndCounterButtonShouldBeClickedTwoTimes()
    {
        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");
        
        var clickedButton = driver.FindElement(By.Id("Clicker"));
        Assert.That(clickedButton.Text, Does.Contain("Click me"));

        clickedButton.Click();
        Assert.That(clickedButton.Text, Does.Contain("Clicked 1 time"));

        clickedButton.Click();
        Assert.That(clickedButton.Text, Does.Contain("Clicked 2 times"));
    }
}
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
    public void AddNewTodo()
    {
        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        var newTodoNameEntry = driver.FindElement(By.Id("NewTodoName"));
        var addTodoButton = driver.FindElement(By.Id("AddTodoButton"));

        newTodoNameEntry.SendKeys("Neues ToDo 123");
        addTodoButton.Click();
        
        var newTodoListviewItem = driver.FindElement(By.Id("neues_todo_123"));
        Assert.That(newTodoListviewItem, Is.Not.Null);
    }
}
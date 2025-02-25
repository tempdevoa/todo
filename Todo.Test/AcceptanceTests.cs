﻿using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;

namespace Todo.AcceptanceTests;

public class AcceptanceTests
{
    private AndroidDriver driver;

    [OneTimeSetUp]
    public void SetUp()
    {        
        driver = AppiumDriverFactory.CreateAndriodDriver();
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

        var newTodoTaskName = $"Akzeptanztest {Guid.NewGuid().ToString("N")}";
        var newTodoTaskId = newTodoTaskName.ToLower().Replace(" ", "_");
        var newTodoNameEntry = driver.FindElement(By.Id("NewTodoName"));
        newTodoNameEntry.SendKeys(newTodoTaskName);

        var addTodoButton = driver.FindElement(By.Id("AddTodoButton"));
        addTodoButton.Click();
        
        var newTodoListviewItem = driver.FindElement(By.Id(newTodoTaskId));
        Assert.That(newTodoListviewItem, Is.Not.Null, "Das neue Todo wurde nicht gefunden.");

        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        newTodoListviewItem = driver.FindElement(By.Id(newTodoTaskId));
        Assert.That(newTodoListviewItem, Is.Not.Null, "Das neue Todo wurde nach neustart der App nicht gefunden und vertmutlich nicht persistiert.");
    }

    [Test]
    public void CompleteNewTodo()
    {
        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        var newTodoTaskName = $"Akzeptanztest {Guid.NewGuid().ToString("N")}";
        var newTodoTaskId = newTodoTaskName.ToLower().Replace(" ", "_");
        var newTodoNameEntry = driver.FindElement(By.Id("NewTodoName"));
        newTodoNameEntry.SendKeys(newTodoTaskName);

        var addTodoButton = driver.FindElement(By.Id("AddTodoButton"));
        addTodoButton.Click();

        var newTodoTaskCheckboxId = $"{newTodoTaskId}checkbox";
        var newTodoCheckbox = driver.FindElement(By.Id(newTodoTaskCheckboxId));
        var isChecked = Convert.ToBoolean(newTodoCheckbox.GetAttribute("checked"));
        Assert.That(isChecked, Is.False, "Das Todo sollte noch nicht abgeschlossen sein.");
        newTodoCheckbox.Click();
                
        isChecked = Convert.ToBoolean(newTodoCheckbox.GetAttribute("checked"));
        Assert.That(isChecked, Is.True, "Das Todo sollte abgeschlossen sein.");

        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        newTodoCheckbox = driver.FindElement(By.Id(newTodoTaskCheckboxId));
        isChecked = Convert.ToBoolean(newTodoCheckbox.GetAttribute("checked"));
        Assert.That(isChecked, Is.True, "Das Todo sollte abgeschlossen sein.");
    }

    [Test]
    public void DeleteNewTodo()
    {
        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        var newTodoTaskName = $"Akzeptanztest {Guid.NewGuid().ToString("N")}";
        var newTodoTaskId = newTodoTaskName.ToLower().Replace(" ", "_");
        var newTodoNameEntry = driver.FindElement(By.Id("NewTodoName"));
        newTodoNameEntry.SendKeys(newTodoTaskName);

        var addTodoButton = driver.FindElement(By.Id("AddTodoButton"));
        addTodoButton.Click();

        var newTodoListviewItem = driver.FindElement(By.Id(newTodoTaskId));
        Assert.That(newTodoListviewItem, Is.Not.Null, "Das neue Todo wurde nicht gefunden.");

        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        newTodoListviewItem = driver.FindElement(By.Id(newTodoTaskId));
        Assert.That(newTodoListviewItem, Is.Not.Null, "Das neue Todo wurde nach neustart der App nicht gefunden und vertmutlich nicht persistiert.");

        driver.ExecuteScript("mobile: swipeGesture", new Dictionary<string, object>()
        {
            { "elementId", newTodoListviewItem.Id },
            { "direction", "right" },
            { "percent", "1" }
        });

        Assert.Throws<NoSuchElementException>(() => driver.FindElement(By.Id(newTodoTaskId)), "Das neue Todo wurde nach neustart der App gefunden und dementsprechend nicht gelöscht.");
    }

    [Ignore("Not working yet")]
    public void RenameNewTodo()
    {
        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");
        driver.HideKeyboard();

        var newTodoTaskName = $"Akzeptanztest {Guid.NewGuid().ToString("N")}";
        var newTodoTaskId = newTodoTaskName.ToLower().Replace(" ", "_");
        var newTodoNameEntry = driver.FindElement(By.Id("NewTodoName"));
        newTodoNameEntry.SendKeys(newTodoTaskName);

        var addTodoButton = driver.FindElement(By.Id("AddTodoButton"));
        addTodoButton.Click();

        var newTodoListviewItem = driver.FindElement(By.Id(newTodoTaskId));
        Assert.That(newTodoListviewItem, Is.Not.Null, "Das neue Todo wurde nicht gefunden.");

        driver.ExecuteScript("mobile: doubleClickGesture", new Dictionary<string, object>()
        {
            { "elementId", newTodoListviewItem.Id }
        });

        var updatedTodoTaskName = $"u_{newTodoTaskName}";
        var editEntryId = $"{newTodoTaskId}titleentry";
        var editEntry = driver.FindElement(By.Id(editEntryId));
        editEntry.Clear();
        editEntry.SendKeys(updatedTodoTaskName);
        
        // Tried different things to trigger the completed event - not working so far.
        // Works in production.

        //editEntry.SendKeys(Keys.Enter);
        //driver.PressKeyCode(new KeyEvent(AndroidKeyCode.Enter));
        //Task.Delay(1000).Wait();

        driver.StartActivity("com.todo.todoapp", "crc642cfc5ea161b91bf0.MainActivity");

        var updatedTodoTitleLabelId = $"{newTodoTaskId}titlelabel";
        var updatedTodoTitleLabel = driver.FindElement(By.Id(updatedTodoTitleLabelId));
        Assert.That(updatedTodoTitleLabel.Text, Is.EqualTo(updatedTodoTaskName), "Das aktualisierte Todo wurde nach neustart der App nicht gefunden und vertmutlich nicht persistiert.");
    }
}
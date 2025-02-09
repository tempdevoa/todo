# Todo App
## Prerequisites
In order to build and test you need to have the following prerequisites:
- Anything you need in order to build and rund .net maui and to develop android apps (for example Android SDK)
- A device emulator
- Appium (for fast and easy setup see the [quickstart](https://appium.io/docs/en/latest/quickstart/))

### Test
1. Set the applicationUrl you want to use for the todo.server in [launchesettings.json](..\Todo\Todo.Server\Properties\launchSettings.json)
2. The same url should be set in as the AppServerIPandPort in the [RestClient.cs](..\Todo\Todo\Gateways\RestClient.cs)
3. Set the DeviceName you emulate in the [AppiumConfiguration.cs](..\Todo\Todo.Test\AppiumConfiguration.cs)
4. Build all
5. Start the appium server in a new CMD or PowerShell using the command "appium"
6. Start your android device emulator
7. Run all (the server and the app)
8. Run all the tests

## License
MIT
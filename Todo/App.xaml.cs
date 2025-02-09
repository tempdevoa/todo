﻿using Todo.Views;

namespace Todo;

public partial class App : Application
{
	private readonly MainPage mainPage;

	public App(MainPage mainPage)
	{
		this.mainPage = mainPage;

        InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(mainPage);
	}
}
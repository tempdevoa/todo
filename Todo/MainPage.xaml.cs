using Todo.ViewModels;

namespace Todo;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}
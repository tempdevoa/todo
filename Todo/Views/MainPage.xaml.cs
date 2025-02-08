using Todo.ViewModels;

namespace Todo.Views;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel viewModel;

	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    private async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = e.Value;
        if (isChecked && viewModel.IsNotBusy)
        {
            var idOfCheckbox = (sender as CheckBox).AutomationId;
            var idOfTodo = idOfCheckbox.Remove(idOfCheckbox.Length - 8);
            await viewModel.CompleteTodoAsync(idOfTodo);
        }
    }
}
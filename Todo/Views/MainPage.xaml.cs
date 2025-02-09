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
        var sendingCheckbox = sender as CheckBox;
        if (sendingCheckbox != null && isChecked && viewModel.IsNotBusy)
        {
            var idOfCheckbox = sendingCheckbox.AutomationId;
            var idOfTodo = idOfCheckbox.Remove(idOfCheckbox.Length - 8);
            await viewModel.CompleteTodoAsync(idOfTodo);
        }
    }

    private void OnTitleLabelTapped(object sender, TappedEventArgs e)
    {
        var sendingLabel = sender as Label;
        if(sendingLabel != null)
        {
            sendingLabel.IsVisible = false;
            var stackLayout = (StackLayout)sendingLabel.Parent;

            var checkBox = (CheckBox)stackLayout.Children[0];
            checkBox.IsEnabled = false;

            var entry = (Entry)stackLayout.Children[2];
            entry.IsVisible = true;
            entry.Focus();
        }
    }

    private void OnTitleEntryCompleted(object sender, EventArgs e)
    {
        var sendingEntry = sender as Entry;
        TitleEntryLeavedAsync(sendingEntry);
    }

    private void OnTitleEntryFocusLost(object sender, FocusEventArgs e)
    {
        var sendingEntry = sender as Entry;
        TitleEntryLeavedAsync(sendingEntry);
    }

    private async void TitleEntryLeavedAsync(Entry? sendingEntry)
    {
        if (sendingEntry != null)
        {
            sendingEntry.IsVisible = false;
            var stackLayout = (StackLayout)sendingEntry.Parent;
            
            var label = (Label)stackLayout.Children[1];
            label.IsVisible = true;

            var checkBox = (CheckBox)stackLayout.Children[0];
            checkBox.IsEnabled = true;

            var todoId = label.AutomationId.Replace("titlelabel", string.Empty);
            await viewModel.RenameTodoAsync(todoId, sendingEntry.Text);
        }
    }
}
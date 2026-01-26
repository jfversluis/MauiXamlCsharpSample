namespace MauiXamlCsharpSample;

public partial class MainPage : ContentPage
{
	public MainViewModel ViewModel { get; }

	public MainPage()
	{
		InitializeComponent();
		ViewModel = new MainViewModel();
		BindingContext = ViewModel;
	}

	public void OnCounterClicked() => ViewModel.ClickCount++;

	// Method called from async lambda in XAML
	public async Task SaveAsync()
	{
		ViewModel.StatusMessage = "Saving...";
		await Task.Delay(1500); // Simulate async operation
		ViewModel.StatusMessage = "Saved successfully!";
	}
}

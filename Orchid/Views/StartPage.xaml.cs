using Orchid.ViewModels;

namespace Orchid.Views;

public partial class StartPage : ContentPage
{
	public StartPage(StartPageViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
	}
}
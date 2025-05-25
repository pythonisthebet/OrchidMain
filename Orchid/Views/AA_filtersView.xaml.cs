using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_filtersView : ContentPage
{
	public AA_filtersView(AA_filtersViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        AA_filtersViewModel _vm = (BindingContext as AA_filtersViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}
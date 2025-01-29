using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_raceView : ContentPage
{
    private readonly AA_raceViewModel _vm;

    public AA_raceView(AA_raceViewModel vm)
    {
        AA_raceViewModel _vm = vm;
        this.BindingContext = vm;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        AA_raceViewModel _vm = (BindingContext as AA_raceViewModel);
        await _vm.InitilizeAsync();// you can have some additional logic to cache the result` 
    }
}
using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_raceView : ContentPage
{
    public AA_raceView(AA_raceViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
using Orchid.ViewModels;

namespace Orchid.Views;

public partial class ChView : ContentPage
{
    public ChView(ChViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
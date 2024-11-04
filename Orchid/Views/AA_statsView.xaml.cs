using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_statsView : ContentPage
{
    public AA_statsView(AA_statsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
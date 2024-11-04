using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_featsView : ContentPage
{
    public AA_featsView(AA_featsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
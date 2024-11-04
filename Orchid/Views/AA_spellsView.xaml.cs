using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_spellsView : ContentPage
{
    public AA_spellsView(AA_spellsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
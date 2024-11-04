using Orchid.ViewModels;

namespace Orchid.Views;

public partial class PartyView : ContentPage
{
    public PartyView(PartyViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
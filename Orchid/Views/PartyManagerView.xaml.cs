using Orchid.ViewModels;

namespace Orchid.Views;

public partial class PartyManagerView : ContentPage
{
    public PartyManagerView(PartyManagerViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
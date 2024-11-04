using Orchid.ViewModels;

namespace Orchid.Views;

public partial class PartyListView : ContentPage
{
    public PartyListView(PartyListViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
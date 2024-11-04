using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_equipmentView : ContentPage
{
    public AA_equipmentView(AA_equipmentViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
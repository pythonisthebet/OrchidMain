using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AA_skillsView : ContentPage
{
    public AA_skillsView(AA_skillsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
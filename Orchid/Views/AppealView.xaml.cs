using Orchid.ViewModels;

namespace Orchid.Views;

public partial class AppealView : ContentPage
{
    public AppealView(AppealViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
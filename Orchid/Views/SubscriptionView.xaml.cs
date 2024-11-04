using Orchid.ViewModels;

namespace Orchid.Views;

public partial class SubscriptionView : ContentPage
{
    public SubscriptionView(SubscriptionViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}
using CommunityToolkit.Maui.Converters;
using Orchid.ViewModels;
using Orchid.Converters;

namespace Orchid.Views;

public partial class PaymentPage : ContentPage
{
	public PaymentPage(PaymentViewModel vm)
	{
        this.BindingContext = vm;
        // Register value converters in resources
        Resources.Add("BoolToColorConverter", new BoolToColorConverter());
        Resources.Add("InverseBoolConverter", new InverseBoolConverter());
        Resources.Add("StatusToColorConverter", new StatusToColorConverter());
        InitializeComponent();
    }
}
using Orchid.ViewModels;

namespace Orchid.Views;

public partial class SignUpView : ContentPage
{
	public SignUpView(SignUpViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}
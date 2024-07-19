using PCDUmapedis.Mobile.ViewModels.Startup;

namespace PCDUmapedis.Mobile.Views.Startup;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}
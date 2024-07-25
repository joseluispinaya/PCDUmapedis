using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Mobile.ViewModels.Startup;

namespace PCDUmapedis.Mobile.Views.Startup;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
        IRepository repository = new Repository();
        BindingContext = new LoginViewModel(repository);
    }
}
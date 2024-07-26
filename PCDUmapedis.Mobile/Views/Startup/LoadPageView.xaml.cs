using PCDUmapedis.Mobile.ViewModels.Startup;

namespace PCDUmapedis.Mobile.Views.Startup;

public partial class LoadPageView : ContentPage
{
	public LoadPageView()
	{
		InitializeComponent();
        BindingContext = new LoadingViewModel();
    }
}
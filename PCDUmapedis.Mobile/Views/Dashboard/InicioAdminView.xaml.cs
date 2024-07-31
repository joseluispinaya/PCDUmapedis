using PCDUmapedis.Mobile.ViewModels.Dashboard;
namespace PCDUmapedis.Mobile.Views.Dashboard;

public partial class InicioAdminView : ContentPage
{
	public InicioAdminView()
	{
		InitializeComponent();
        BindingContext = new InicioAdminViewModel();
    }
}
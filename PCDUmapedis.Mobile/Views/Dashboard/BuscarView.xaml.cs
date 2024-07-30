using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Mobile.ViewModels.Dashboard;

namespace PCDUmapedis.Mobile.Views.Dashboard;

public partial class BuscarView : ContentPage
{
	public BuscarView()
	{
		InitializeComponent();
        IRepository repository = new Repository();
        BindingContext = new BuscarViewModel(repository);
    }
}
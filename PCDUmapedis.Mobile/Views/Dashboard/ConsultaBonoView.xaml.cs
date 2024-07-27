using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Mobile.ViewModels.Dashboard;

namespace PCDUmapedis.Mobile.Views.Dashboard;

public partial class ConsultaBonoView : ContentPage
{
	public ConsultaBonoView()
	{
		InitializeComponent();
        IRepository repository = new Repository();
        BindingContext = new ConsultaBonoViewModel(repository);
    }
}
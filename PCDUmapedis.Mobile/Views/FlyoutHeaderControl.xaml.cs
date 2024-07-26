using PCDUmapedis.Mobile.ViewModels;

namespace PCDUmapedis.Mobile.Views;

public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();
        BindingContext = new FlyoutHeaderControlModel();
    }
}
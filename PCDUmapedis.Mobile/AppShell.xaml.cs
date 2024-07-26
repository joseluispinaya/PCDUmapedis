using PCDUmapedis.Mobile.ViewModels;
using PCDUmapedis.Mobile.Views.Dashboard;

namespace PCDUmapedis.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            Routing.RegisterRoute(nameof(InicioView), typeof(InicioView));
        }
    }
}

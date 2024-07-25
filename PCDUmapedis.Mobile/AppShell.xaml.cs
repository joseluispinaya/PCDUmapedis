using PCDUmapedis.Mobile.Views.Dashboard;

namespace PCDUmapedis.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(InicioView), typeof(InicioView));
        }
    }
}

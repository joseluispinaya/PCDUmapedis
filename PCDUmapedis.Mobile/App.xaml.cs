using PCDUmapedis.Mobile.Views.Dashboard;
using PCDUmapedis.Mobile.Views.Startup;

namespace PCDUmapedis.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new InicioView();
            //MainPage = new AppShell();
        }
    }
}

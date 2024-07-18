using PCDUmapedis.Mobile.Views.Startup;

namespace PCDUmapedis.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
            //MainPage = new AppShell();
        }
    }
}

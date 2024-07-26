using PCDUmapedis.Mobile.Views;
using PCDUmapedis.Mobile.Views.Dashboard;
using PCDUmapedis.Mobile.Views.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCDUmapedis.Mobile.ViewModels.Startup
{
    public class LoadingViewModel
    {
        public LoadingViewModel() 
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            var sesi = await SecureStorage.Default.GetAsync(SettingsConst.Logi);
            if (string.IsNullOrEmpty(sesi))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
            }
            else
            {
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
            }
        }
    }
}

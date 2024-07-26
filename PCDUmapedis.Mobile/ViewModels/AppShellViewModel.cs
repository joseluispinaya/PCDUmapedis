using PCDUmapedis.Mobile.Views.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCDUmapedis.Mobile.ViewModels
{
    public class AppShellViewModel
    {
        public ICommand SalirCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await SalirCommandAsync();
                });
            }
        }
        private async Task SalirCommandAsync()
        {
            SecureStorage.Default.Remove(SettingsConst.Logi);
            SecureStorage.Default.Remove(SettingsConst.Userl);

            await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
        }
    }
}

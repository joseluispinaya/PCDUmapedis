using PCDUmapedis.Mobile.Views.Dashboard;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCDUmapedis.Mobile.ViewModels.Startup
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
        public bool IsRunning { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await KLoginCommandAsync();
                });
            }
        }

        private async Task KLoginCommandAsync()
        {
            IsRunning = true;

            if (Email != "jo")
            {
                IsRunning = false;
                //App.Current?.MainPage?.DisplayAlert("Error", "En Usuario", "Ok");
                await Shell.Current.DisplayAlert("Error", "En Usuario", "Ok");
                return;
            }
            IsRunning = false;
            await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
        }
    }
}

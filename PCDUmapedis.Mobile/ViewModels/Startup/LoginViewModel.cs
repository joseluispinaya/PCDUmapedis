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
                return new Command(() =>
                {
                    KLoginCommandAsync();
                });
            }
        }

        private void KLoginCommandAsync()
        {
            IsRunning = true;

            if (Email != "jose@gmail.com")
            {
                IsRunning = false;
                App.Current?.MainPage?.DisplayAlert("Error", "En Usuario", "Ok");
                return;
            }
            IsRunning = false;
            App.Current?.MainPage?.DisplayAlert("Bien", "En Usuario", "Ok");
        }
    }
}

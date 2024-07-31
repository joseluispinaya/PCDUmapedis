using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCDUmapedis.Mobile.ViewModels.Dashboard
{
    [AddINotifyPropertyChangedInterface]
    public class InicioAdminViewModel
    {
        public InicioAdminViewModel()
        {
                
        }

        public bool IsRunning { get; set; }

        public ICommand BuscaraCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ExecuteBuscaraCommandAsync();
                });
            }
        }

        private async Task ExecuteBuscaraCommandAsync()
        {
            IsRunning = true;

            // Espera de 3 segundos
            await Task.Delay(3000);
            IsRunning = false;
            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);

            if (!string.IsNullOrEmpty(use))
            {
                await Shell.Current.DisplayAlert("Bien", "Exelente.", "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Malllll.", "Ok");
            }

        }
    }
}

using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Mobile.Views.Dashboard;
using PCDUmapedis.Shared.DTOs;
using PCDUmapedis.Shared.Models;
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
        private readonly IRepository _repository;
        public LoginViewModel(IRepository repository)
        {
            _repository = repository;
        }

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
            string url = "https://umapedis-001-site1.ftempurl.com/";

            LoginDTO loginDTO = new LoginDTO
            {
                Ciperso = "123456",
                Codcarnetdisca = "111213"
            };

            var httpResponse = await _repository.GetPersoN<ResponsePCD>(url, "api/pagobonos/Login", loginDTO);
            IsRunning = false;

            if (httpResponse.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");

            ResponsePCD responsePCD = httpResponse.Response;
            //string userDetail = JsonConvert.SerializeObject(responsePCD);

            await Shell.Current.GoToAsync($"//{nameof(InicioView)}");

            //if (Email != "jo")
            //{
            //    IsRunning = false;
            //    await Shell.Current.DisplayAlert("Error", "En Usuario", "Ok");
            //    return;
            //}

        }
    }
}

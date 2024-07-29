using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Mobile.Views.Dashboard;
using PCDUmapedis.Shared.DTOs;
using PCDUmapedis.Shared.Models;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PCDUmapedis.Mobile.Views;
using PCDUmapedis.Mobile.Modei;

namespace PCDUmapedis.Mobile.ViewModels.Startup
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        private readonly IRepository _repository;
        public LoginViewModel(IRepository repository)
        {
            _repository = repository;
            IsToggled = false;
        }

        public string? Email { get; set; }

        public string? Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsToggled { get; set; }


        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await KLoginCommandAsyncIa();
                });
            }
        }

        private UserBasicInfo? ConvertToUserBasicInfo<T>(T response) where T : class
        {
            if (response == null)
            {
                // Manejo de la respuesta nula si es necesario
                return null; // o return new UserBasicInfo { ... };
            }

            return response switch
            {
                ResponsePCD responsePCD => new UserBasicInfo
                {
                    IdUsuaLog = responsePCD.Idpersodisca,
                    Nombre = responsePCD.Nombres,
                    Apellido = responsePCD.Apellidos,
                    Foto = responsePCD.Foto,
                    RolId = (int)RolDetails.PcdUsu,
                    RolTex = "Persona Pcd"
                },
                ResponseUsua responseUsua => new UserBasicInfo
                {
                    IdUsuaLog = responseUsua.IdUsuario,
                    Nombre = responseUsua.Nombres,
                    Apellido = responseUsua.Apellidos,
                    Foto = responseUsua.Foto,
                    RolId = (int)RolDetails.Admin,
                    RolTex = "Persona Admin"
                },
                _ => throw new InvalidOperationException("Tipo de respuesta no manejado")
            };
        }

        private async Task<UserBasicInfo?> LoginAsync<T>(string endpoint, LoginDTO loginDTO) where T : class
        {
            IsRunning = true;
            string url = "https://umapedis-001-site1.ftempurl.com/";
            var httpResponse = await _repository.GetPersoN<T>(url, endpoint, loginDTO);
            IsRunning = false;

            if (httpResponse.Error)
            {
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                Password = string.Empty;
                return null;
            }

            var response = httpResponse.Response;
            if (response == null)
            {
                // Manejo adicional del caso nulo si es necesario
                await Shell.Current.DisplayAlert("Error", "La respuesta del servidor es nula.", "Ok");
                return null;
            }
            return ConvertToUserBasicInfo(response);
        }

        private Task<UserBasicInfo?> MetodoPcdIa()
        {
            var loginDTO = new LoginDTO { Ciperso = "123456", Codcarnetdisca = "111213" };
            return LoginAsync<ResponsePCD>("api/pagobonos/Login", loginDTO);
        }

        private Task<UserBasicInfo?> MetodoAdminIa()
        {
            var loginDTO = new LoginDTO { Ciperso = "manuel@yopmail.com", Codcarnetdisca = "aedf44" };
            return LoginAsync<ResponseUsua>("api/pagobonos/Loginusua", loginDTO);
        }

        private async Task KLoginCommandAsyncIa()
        {
            UserBasicInfo? userBasicInfo;

            if (!IsToggled)
            {
                userBasicInfo = await MetodoPcdIa();
            }
            else
            {
                userBasicInfo = await MetodoAdminIa();
            }

            if (userBasicInfo == null)
            {
                await Shell.Current.DisplayAlert("Error", "La Intente mas tarde.", "Ok");
                return;
            }

            await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");
            string userDetail = JsonConvert.SerializeObject(userBasicInfo);
            await SecureStorage.Default.SetAsync(SettingsConst.Userl, userDetail);

            await AppConstant.AddFlyoutMenusDetails();

            //AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            //await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
        }



        private async Task<UserBasicInfo?> MetodoAdmin()
        {
            IsRunning = true;
            string url = "https://umapedis-001-site1.ftempurl.com/";
            LoginDTO loginDTO = new LoginDTO
            {
                Ciperso = "manuel@yopmail.com",
                Codcarnetdisca = "aedf44"
            };

            var httpResponse = await _repository.GetPersoN<ResponseUsua>(url, "api/pagobonos/Loginusua", loginDTO);
            IsRunning = false;

            if (httpResponse.Error)
            {
                //IsRunning = false;
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                Password = string.Empty;
                return null;
            }
            var responseAdmin = httpResponse.Response!;
            var userBasicInfo = new UserBasicInfo
            {
                IdUsuaLog = responseAdmin.IdUsuario,
                Nombre = responseAdmin.Nombres,
                Apellido = responseAdmin.Apellidos,
                Foto = responseAdmin.Foto,
                RolId = (int)RolDetails.Admin,
                RolTex = "Persona Admin"
            };
            return userBasicInfo;
        }
        private async Task<UserBasicInfo?> MetodoPcd()
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
                //IsRunning = false;
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                Password = string.Empty;
                return null;
            }
            var responsePCD = httpResponse.Response!;
            var userBasicInfo = new UserBasicInfo
            {
                IdUsuaLog = responsePCD.Idpersodisca,
                Nombre = responsePCD.Nombres,
                Apellido = responsePCD.Apellidos,
                Foto = responsePCD.Foto,
                RolId = (int)RolDetails.PcdUsu,
                RolTex = "Persona Pcd"
            };

            return userBasicInfo;

        }

        private async Task KLoginCommandAsynaaaac()
        {
            if (!IsToggled)
            {
                //await Shell.Current.DisplayAlert("Error", "Es PCD", "Ok");
                var userBasicInfo = await MetodoPcd();

                if (userBasicInfo == null)
                {
                    // Manejar el caso de error o valor nulo
                    return;
                }

                await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");
                string userDetail = JsonConvert.SerializeObject(userBasicInfo);
                await SecureStorage.Default.SetAsync(SettingsConst.Userl, userDetail);

                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync($"//{nameof(InicioView)}");

            }
            else
            {
                //await Shell.Current.DisplayAlert("Error", "Es Usuario", "Ok");
                // si es admin
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
            

            if (httpResponse.Error)
            {
                IsRunning = false;
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                Password = string.Empty;
                return;
            }

            await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");

            ResponsePCD responsePCD = httpResponse.Response!;
            string userDetail = JsonConvert.SerializeObject(responsePCD);
            await SecureStorage.Default.SetAsync(SettingsConst.Userl, userDetail);

            IsRunning = false;

            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            await Shell.Current.GoToAsync($"//{nameof(InicioView)}");


        }
    }
}

using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Shared.Models;
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
    public class BuscarViewModel
    {
        private readonly IRepository _repository;
        public BuscarViewModel(IRepository repository)
        {
            _repository = repository;
            Foto = "sinpcd";
        }

        public ResponsePCD? Persona { get; set; }
        public string? Cedula { get; set; }
        public ImageSource? Foto { get; set; }
        public bool IsRunning { get; set; }

        public ICommand BuscarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ExecuteBuscarCommandAsync();
                });
            }
        }

        private async Task ExecuteBuscarCommandAsync()
        {
            if (string.IsNullOrEmpty(Cedula))
            {
                await Shell.Current.DisplayAlert("Error", "Debe Ingresar Numero de CI.", "Ok");
                return;
            }

            IsRunning = true;
            string url = "https://umapedis-001-site1.ftempurl.com/";

            //if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            //IsRunning = false;
            //await Shell.Current.DisplayAlert("Error", "No hay conexión a Internet", "Ok");
            //return

            var httpResponse = await _repository.GetPcd<ResponsePCD>(Cedula, url, "api", "/pagobonos/buscar");
            IsRunning = false;

            if (httpResponse.Error)
            {
                Persona = null;
                Foto = "sinpcd";
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }
            Persona = httpResponse.Response;
            Foto = Persona?.PictureFullPath;
        }
    }
}

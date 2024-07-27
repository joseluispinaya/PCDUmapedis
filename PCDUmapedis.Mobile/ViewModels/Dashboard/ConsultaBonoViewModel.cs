using PCDUmapedis.Mobile.Repositories;
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

namespace PCDUmapedis.Mobile.ViewModels.Dashboard
{
    [AddINotifyPropertyChangedInterface]
    public class ConsultaBonoViewModel
    {
        private readonly IRepository _repository;

        public ConsultaBonoViewModel(IRepository repository)
        {
            _repository = repository;
            LoadGestiones();
        }

        public EGestion? Gestion { get; set; }
        public List<EGestion>? Gestiones { get; set; }
        public List<ResponseConsultaB>? PagopcdBonos { get; set; }

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


        private async void LoadGestiones()
        {
            string url = "https://umapedis-001-site1.ftempurl.com/";
            var responseHttp = await _repository.Get<List<EGestion>>(url, "api/gestiones/combo");
            //var httpResponse = await repository.Get<List<TipoPersonal>>("api/gestiones/combo");

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            Gestiones = responseHttp.Response;
        }

        private async Task ExecuteBuscarCommandAsync()
        {
            if (Gestion == null)
            {
                await Shell.Current.DisplayAlert("Error", "Seleccione una Gestion", "Ok");
                return;
            }

            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);
            if (string.IsNullOrEmpty(use))
            {
                await Shell.Current.DisplayAlert("Error", "Ocurrio un error Inicie sesion", "Ok");
                return;
            }

            var pcd = JsonConvert.DeserializeObject<ResponsePCD>(use);
            if (pcd == null)
            {
                await Shell.Current.DisplayAlert("Error", "Ocurrio un error Inicie sesion", "Ok");
                return;
            }

            string url = "https://umapedis-001-site1.ftempurl.com/";

            ConsultaDTO consultaDTO = new ConsultaDTO
            {
                Idpersodisca = pcd.Idpersodisca,
                Idges = Gestion.Idges
            };
            //var responseHttp = await _repository.Get<List<EGestion>>(url, "api/gestiones/combo");
            var responseHttp = await _repository.GetPagosN<List<ResponseConsultaB>>(url, "api/pagobonos/Consulta", consultaDTO);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            PagopcdBonos = responseHttp.Response;
            //var can = PagopcdBonos?.Count.ToString();
            //await Shell.Current.DisplayAlert("Error", "Tolal Lista: " + can, "Ok");
        }
    }
}

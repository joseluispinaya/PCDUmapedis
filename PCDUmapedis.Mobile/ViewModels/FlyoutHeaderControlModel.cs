using PCDUmapedis.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace PCDUmapedis.Mobile.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class FlyoutHeaderControlModel
    {
        public FlyoutHeaderControlModel()
        {
            LoadPersonalAsync();
        }

        public ResponsePCD? User { get; set; }
        public string? Email { get; set; }

        public string? Nombre { get; set; }
        public string? Foto { get; set; }


        private async void LoadPersonalAsync()
        {
            await InicioAsync();
        }

        private async Task InicioAsync()
        {
            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);
            User = JsonConvert.DeserializeObject<ResponsePCD>(use);
            Nombre = User?.Nombres;
            Email = User?.Apellidos;
            Foto = User?.PictureFullPath;
        }
    }
}

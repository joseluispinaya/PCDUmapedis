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

        public UserBasicInfo? User { get; set; }
        //public ResponsePCD? User { get; set; }
        public string? Email { get; set; }

        public string? Nombre { get; set; }
        public string? Foto { get; set; }


        private async void LoadPersonalAsync()
        {
            await InicioAsync();
        }
        //string.IsNullOrEmpty(use)
        private async Task InicioAsync()
        {
            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);
            if (!string.IsNullOrEmpty(use))
            {
                User = JsonConvert.DeserializeObject<UserBasicInfo>(use);
                Nombre = User?.Nombre;
                Email = User?.Apellido;
                Foto = User?.PictureFullPath;
            }
            
        }

        //private async Task InicioAsync()
        //{
        //    var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);
        //    User = JsonConvert.DeserializeObject<ResponsePCD>(use);
        //    Nombre = User?.Nombres;
        //    Email = User?.Apellidos;
        //    Foto = User?.PictureFullPath;
        //}
    }
}

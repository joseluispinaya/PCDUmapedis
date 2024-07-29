using PCDUmapedis.Mobile.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCDUmapedis.Shared.Models;
using PCDUmapedis.Mobile.Views.Dashboard;
using Android.Graphics.Drawables;

namespace PCDUmapedis.Mobile.Modei
{
    public class AppConstant
    {
        public async static Task AddFlyoutMenusDetails()
        {
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();

            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);

            var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(use);

            var pcdDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(InicioView)).FirstOrDefault();
            if (pcdDashboardInfo != null) AppShell.Current.Items.Remove(pcdDashboardInfo);

            var adminDashboardInfo = AppShell.Current.Items.Where(f => f.Route == nameof(InicioAdminView)).FirstOrDefault();
            if (adminDashboardInfo != null) AppShell.Current.Items.Remove(adminDashboardInfo);

            if (userInfo?.RolId == (int)RolDetails.PcdUsu)
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Inicio PCD",
                    Route = nameof(InicioView),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = Icons.Dashboard,
                            Title = "PCD Dashboard",
                            ContentTemplate = new DataTemplate(typeof(InicioView)),
                        },
                        new ShellContent
                        {
                            Icon = Icons.ListaP,
                            Title = "Consulta Bono",
                            ContentTemplate = new DataTemplate(typeof(ConsultaBonoView)),
                        }
                    }
                };

                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                    await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
                }
            }

            if (userInfo?.RolId == (int)RolDetails.Admin)
            {
                var flyoutItem = new FlyoutItem()
                {
                    Title = "Inicio Funcio",
                    Route = nameof(InicioAdminView),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = Icons.Dashboard,
                            Title = "Inicio Admin",
                            ContentTemplate = new DataTemplate(typeof(InicioAdminView)),
                        },
                        new ShellContent
                        {
                            Icon = Icons.AboutUs,
                            Title = "Buscar PCD",
                            ContentTemplate = new DataTemplate(typeof(BuscarView)),
                        }
                    }
                };

                if (!AppShell.Current.Items.Contains(flyoutItem))
                {
                    AppShell.Current.Items.Add(flyoutItem);
                    await Shell.Current.GoToAsync($"//{nameof(InicioAdminView)}");
                }
            }
        }
    }
}

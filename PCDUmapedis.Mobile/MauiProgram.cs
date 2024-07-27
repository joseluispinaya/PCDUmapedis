using Plugin.Firebase.Auth;
using Plugin.Firebase.Bundled.Shared;
using Microsoft.Extensions.Logging;
using Plugin.Firebase.Crashlytics;
using Microsoft.Maui.LifecycleEvents;
using PCDUmapedis.Mobile.Views.Dashboard;
using PCDUmapedis.Mobile.Views.Startup;
using PCDUmapedis.Mobile.ViewModels.Startup;
using PCDUmapedis.Mobile.Repositories;
using PCDUmapedis.Mobile.Views;
using PCDUmapedis.Mobile.ViewModels;
using PCDUmapedis.Mobile.ViewModels.Dashboard;





#if IOS
using Plugin.Firebase.Bundled.Platforms.iOS;
#else
using Plugin.Firebase.Bundled.Platforms.Android;
#endif


namespace PCDUmapedis.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IRepository, Repository>();
            //Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<InicioView>();
            builder.Services.AddTransient<LoadPageView>();
            builder.Services.AddTransient<ConsultaBonoView>();
            builder.Services.AddTransient<FlyoutHeaderControl>();
            

            //View Models
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LoadingViewModel>();
            builder.Services.AddTransient<ConsultaBonoViewModel>();
            builder.Services.AddTransient<FlyoutHeaderControlModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if IOS
            events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                CrossFirebase.Initialize(CreateCrossFirebaseSettings());
                return false;
            }));
#else
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                    CrossFirebase.Initialize(activity, CreateCrossFirebaseSettings())));
                CrossFirebaseCrashlytics.Current.SetCrashlyticsCollectionEnabled(true);
#endif
            });

            builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
            return builder;
        }

        private static CrossFirebaseSettings CreateCrossFirebaseSettings()
        {
            return new CrossFirebaseSettings(isAuthEnabled: true,
            isCloudMessagingEnabled: true, isAnalyticsEnabled: true);
        }
    }
}

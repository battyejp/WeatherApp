using System;

using Android.App;
using Android.Runtime;
using Ninject;
using Ninject.Modules;
using WeatherApp.Common.Models;
using WeatherApp.Common.Services;
using WeatherApp.Common.Services.Interfaces;

namespace WeatherApp
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          : base(handle, transer)
        {
            var settings = new NinjectSettings() { LoadExtensions = false };
            Kernel = new StandardKernel(settings, new NinjectModuleImplementation());
        }

        public static IKernel Kernel { get; private set; }

        public override void OnCreate()
        {
            base.OnCreate();

            Kernel.Get<IDataService<Location>>().Setup();
            Kernel.Get<IDataService<DailyWeather>>().Setup();
        }
    }
    internal class NinjectModuleImplementation : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileService>().To<FileService>();
            Bind<IWeatherService>().To<WeatherService>();
            Bind<IDataService<Location>>().To<DataService<Location>>().InSingletonScope();
            Bind<IDataService<DailyWeather>>().To<DataService<DailyWeather>>().InSingletonScope();
        }
    }
}
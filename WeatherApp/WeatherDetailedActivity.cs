using Android.App;
using Android.OS;
using Android.Widget;
using Ninject;
using System.Linq;
using WeatherApp.Common.Models;
using WeatherApp.Common.Services.Interfaces;
using WeatherApp.Controls;

namespace WeatherApp
{
    [Activity]
    public class WeatherDetailedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeatherDetailScroll);
            Title = Intent.GetStringExtra("Title");

            var layout = FindViewById<LinearLayout>(Resource.Id.scrollLayout);

            int selectedLocationId = Intent.GetIntExtra("LocationId", 0);
            var service = MainApplication.Kernel.Get<IDataService<DailyWeather>>();
            var forecast = service.GetAll().Where(x => x.WoeId == selectedLocationId);

            foreach(var weather in forecast)
            {
                layout.AddView(new DailyWeatherCtrl(this, weather));
            }
        }
    }
}
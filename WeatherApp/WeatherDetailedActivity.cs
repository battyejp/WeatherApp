using Android.App;
using Android.OS;
using Android.Widget;
using Ninject;
using WeatherApp.Common.Models;
using WeatherApp.Common.Services.Interfaces;
using WeatherApp.Controls;

namespace WeatherApp
{
    [Activity(Label = "Weekly Forecast")] //TODO set this to location
    public class WeatherDetailedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeatherDetailScroll);

            var layout = FindViewById<LinearLayout>(Resource.Id.scrollLayout);

            int selectedLocationId = Intent.GetIntExtra("LocationId", 0); //TODO use this to filter
            var service = MainApplication.Kernel.Get<IDataService<DailyWeather>>();
            var forecast = service.GetAll();

            foreach(var weather in forecast)
            {
                layout.AddView(new DailyWeatherCtrl(this, weather));
            }

            //TODO display all weather
        }
    }
}
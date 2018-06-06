using Android.App;
using Android.OS;
using Android.Widget;
using Ninject;
using WeatherApp.Common.Models;
using WeatherApp.Common.Services.Interfaces;
using WeatherApp.Controls;

namespace WeatherApp
{
    [Activity(Label = "Weekly Forecast")]
    public class WeatherDetailedActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeatherDetail);

            var layout = FindViewById<LinearLayout>(Resource.Id.mainLayout); //TODO add in images
            layout.AddView(new DailyWeatherCtrl(this));

            int selectedLocationId = Intent.GetIntExtra("LocationId", 0);
            var service = MainApplication.Kernel.Get<IDataService<DailyWeather>>();
            var forecast = await service.GetAll();

            //TODO display all weather
        }
    }
}
using Android.App;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WeatherApp.Common.Dtos;
using WeatherApp.Controls;

namespace WeatherApp
{
    [Activity(Label = "Weekly Forecast")]
    public class WeatherDetailedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WeatherDetail);

            string weatherForecastStr = Intent.GetStringExtra("WeatherForecast");

            try
            {
                var weatherForecast = JsonConvert.DeserializeObject<List<DailyWeather>>(weatherForecastStr);
            }
            catch(Exception ex)
            {

            }

            var layout = FindViewById<LinearLayout>(Resource.Id.mainLayout); //TODO add in images
            layout.AddView(new DailyWeather(this));
        }
    }
}
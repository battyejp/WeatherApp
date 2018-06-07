using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WeatherApp.Common.Models;

namespace WeatherApp.Controls
{
    public class DailyWeatherCtrl : LinearLayout
    {
        private View weatherDetailView;

        public DailyWeatherCtrl(Context context, DailyWeather weather)
            : base (context)
        {
            LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
            weatherDetailView = inflater.Inflate(Resource.Layout.WeatherDetail, null);

            SetTextViewText(Resource.Id.tvDate, weather.Date.ToShortDateString()); //TODO better date format
            SetTextViewText(Resource.Id.tvWeather, weather.Weather); //TODO add in images
            SetTextViewText(Resource.Id.tvMax, $"{String.Format("{0:0.0}", weather.MaxTemp)}°");
            SetTextViewText(Resource.Id.tvMin, $"{String.Format("{0:0.0}", weather.MinTemp)}°");

            AddView(weatherDetailView);
        }

        private void SetTextViewText(int resourceId, string text)
        {
            var tv = weatherDetailView.FindViewById<TextView>(resourceId);
            tv.Text = text;
        }
    }
}
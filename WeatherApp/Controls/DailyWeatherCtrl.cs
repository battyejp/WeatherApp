using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
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

            var ivWeather = weatherDetailView.FindViewById<ImageView>(Resource.Id.ivWeather);
            var imageBitmap = GetImageBitmapFromUrl($"https://www.metaweather.com/static/img/weather/png/{weather.WeatherAbbr}.png");
            ivWeather.SetImageBitmap(imageBitmap);

            SetTextViewText(Resource.Id.tvDate, weather.Date.ToString("ddd"));
            SetTextViewText(Resource.Id.tvWeather, weather.Weather);
            SetTextViewText(Resource.Id.tvMax, $"{String.Format("{0:0.0}", weather.MaxTemp)}°");
            SetTextViewText(Resource.Id.tvMin, $"{String.Format("{0:0.0}", weather.MinTemp)}°");

            AddView(weatherDetailView);
        }

        private void SetTextViewText(int resourceId, string text)
        {
            var tv = weatherDetailView.FindViewById<TextView>(resourceId);
            tv.Text = text;
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return Bitmap.CreateScaledBitmap(imageBitmap, imageBitmap.Width / 2, imageBitmap.Height / 2, true);
        }
    }
}
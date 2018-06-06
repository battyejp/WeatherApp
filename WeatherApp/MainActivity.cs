using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;
using Android.Content;
using WeatherApp.Common;
using Newtonsoft.Json;
using System.Linq;
using WeatherApp.Common.Models;

namespace WeatherApp
{
    [Activity(Label = "Weather", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView lvLocations;
        private ProgressBar pgLocations;
        private LocationsViewAdapter locationViewAdaptor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            var refreshBtn = FindViewById<Button>(Resource.Id.btnRefresh);
            refreshBtn.Click += RefreshBtn_Click;

            pgLocations = FindViewById<ProgressBar>(Resource.Id.pgLocations);
            pgLocations.Visibility = ViewStates.Gone;

            lvLocations = FindViewById<ListView>(Resource.Id.lvLocations);
            lvLocations.ItemClick += LvLocations_ItemClick;
        }

        //TODO see if everything below here can be moved
        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            pgLocations.Visibility = ViewStates.Visible;
            var service = new WeatherService(); //TODO use DI to get this
            var results = await service.GetLocationsAsync(); //TODO get current location and pass in
            pgLocations.Visibility = ViewStates.Gone;
            //TODO persist this to sql lite db

            if (results == null)
            {
                Toast.MakeText(this, "Error retrieving locations from search api", ToastLength.Short).Show();
                return;
            }

            locationViewAdaptor = new LocationsViewAdapter(this, results);
            lvLocations.Adapter = locationViewAdaptor;
        }

        private async void LvLocations_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            pgLocations.Visibility = ViewStates.Visible;
            var location = locationViewAdaptor[e.Position];
            var service = new WeatherService(); //TODO use DI to get this
            var weatherForecast = await service.GetWeatherForecastAsync(); //TODO get current location and pass in
            pgLocations.Visibility = ViewStates.Gone;

            if (weatherForecast == null)
            {
                Toast.MakeText(this, "Error retrieving locations from search api", ToastLength.Short).Show();
                return;
            }

            var intent = new Intent(this, typeof(WeatherDetailedActivity));
            var list = weatherForecast.Consolidated_Weather.Select(x => (DailyWeather)x).ToList();
            intent.PutExtra("WeatherForecast", JsonConvert.SerializeObject(list));
            StartActivity(intent);
        }
    }
}


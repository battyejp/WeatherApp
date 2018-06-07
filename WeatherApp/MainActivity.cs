using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views;
using Android.Content;
using WeatherApp.Common;
using System.Linq;
using WeatherApp.Common.Models;
using System.Collections.Generic;
using Ninject;
using WeatherApp.Common.Services.Interfaces;

namespace WeatherApp
{
    [Activity(Label = "Weather", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView lvLocations;
        private ProgressBar pgLocations;
        private LocationsViewAdapter locationViewAdaptor;
        private IDataService<Location> locationService;
        private IDataService<DailyWeather> dailyWeatherService;
        private IList<Location> locations;
        private LocationManager locationManager;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            this.locationManager = new LocationManager(this);
            var refreshBtn = FindViewById<Button>(Resource.Id.btnRefresh);
            refreshBtn.Click += RefreshBtn_Click;

            pgLocations = FindViewById<ProgressBar>(Resource.Id.pgLocations);
            pgLocations.Visibility = ViewStates.Gone;

            lvLocations = FindViewById<ListView>(Resource.Id.lvLocations);
            lvLocations.ItemClick += LvLocations_ItemClick;

            locationService = MainApplication.Kernel.Get<IDataService<Location>>();
            dailyWeatherService = MainApplication.Kernel.Get<IDataService<DailyWeather>>();

            //await locationService.DeleteAll(); //This is used for testing

            locations = locationService.GetAll();
            SetListViewItems();

            await locationManager.RequestLocationUpdatesAsync(this); //TODO check permissions and google services

        }

        private void SetListViewItems() //TODO is this the correct way to refresh
        {
            locationViewAdaptor = new LocationsViewAdapter(this, locations);
            lvLocations.Adapter = locationViewAdaptor;
        }

        //TO DO move everything below here to a viewmodel so it can be unit tested.
        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            var userlocation = await locationManager.GetLastLocationFromDevice();

            if (userlocation == null)
            {
                Toast.MakeText(this, "Error retrieving your location, make sure location is set in your settings", ToastLength.Long).Show();
                return;
            }

            pgLocations.Visibility = ViewStates.Visible;
            var service = new WeatherService(); //TODO use DI to get this
            var results = await service.GetLocationsAsync(userlocation.Latitude, userlocation.Longitude);
            pgLocations.Visibility = ViewStates.Gone;

            locations = results.Select(x => (Location)x).ToList();
            SetListViewItems();

            locationService.RefreshData(locations);

            if (results == null)
            {
                Toast.MakeText(this, "Error retrieving locations from search api", ToastLength.Short).Show();
                return;
            }
        }

        private async void LvLocations_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            pgLocations.Visibility = ViewStates.Visible;
            var location = locationViewAdaptor[e.Position];
            var service = new WeatherService(); //TODO use DI to get this
            var weatherForecast = await service.GetWeatherForecastAsync(location.WoeId);
            pgLocations.Visibility = ViewStates.Gone;

            if (weatherForecast == null)
            {
                Toast.MakeText(this, "Error retrieving locations from search api", ToastLength.Short).Show();
                return;
            }

            var intent = new Intent(this, typeof(WeatherDetailedActivity));
            var list = weatherForecast.Consolidated_Weather.Select(x => (DailyWeather)x).ToList();
            list.ForEach(x => x.WoeId = location.Id);
            dailyWeatherService.RefreshData(list);
            intent.PutExtra("Title", location.Title);
            intent.PutExtra("LocationId", location.Id);
            StartActivity(intent);
        }

        //TODO disable controls when loading icon appears
    }
}


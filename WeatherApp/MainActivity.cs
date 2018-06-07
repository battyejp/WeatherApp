using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using System.Linq;
using WeatherApp.Common.Models;
using System.Collections.Generic;
using Ninject;
using WeatherApp.Common.Services.Interfaces;
using System.Threading.Tasks;

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
        private IWeatherService weatherService;
        private IList<Location> locations;
        private LocationManager locationManager;
        private ProgressDialog progressDialog;

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            this.locationManager = new LocationManager(this);
            var refreshBtn = FindViewById<Button>(Resource.Id.btnRefresh);
            refreshBtn.Click += RefreshBtn_Click;

            lvLocations = FindViewById<ListView>(Resource.Id.lvLocations);
            lvLocations.ItemClick += LvLocations_ItemClick;

            locationService = MainApplication.Kernel.Get<IDataService<Location>>();
            dailyWeatherService = MainApplication.Kernel.Get<IDataService<DailyWeather>>();
            weatherService = MainApplication.Kernel.Get<IWeatherService>();

            //locationService.DeleteAll(); //This is used for testing

            locations = locationService.GetAll();
            locationViewAdaptor = new LocationsViewAdapter(this, locations);
            lvLocations.Adapter = locationViewAdaptor;

            await locationManager.RequestLocationUpdatesAsync(this);

            progressDialog = new ProgressDialog(this)
            {
                Indeterminate = true
            };
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetMessage("Loading...");
            progressDialog.SetCancelable(false);
        }

        //TO DO move as much as possible below here to a viewmodel so that it can be unit tested
        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            if (!locationManager.HasPermissions(this))
            {
                Toast.MakeText(this, "The app needs permissions to location services.", ToastLength.Long).Show();
                return;
            }

            if  (!locationManager.HasGooglePlayServicesInstalled(this))
            {
                Toast.MakeText(this, "The phone does not have google play services installed.", ToastLength.Long).Show();
                return;
            }

            progressDialog.Show();

            var userlocation = await locationManager.GetLastLocationFromDevice();

            if (userlocation == null)
            {
                progressDialog.Cancel();
                Toast.MakeText(this, "Error retrieving your location, make sure location is set in your settings", ToastLength.Long).Show();
                return;
            }

            await GetAndPopulateLocations(userlocation);
        }

        private async Task GetAndPopulateLocations(Android.Locations.Location userlocation)
        {
            var results = await weatherService.GetLocationsAsync(userlocation.Latitude, userlocation.Longitude);

            locations = results.Select(x => (Location)x).ToList();
            locationViewAdaptor.Refresh(locations);
            locationService.RefreshData(locations);
            progressDialog.Cancel();

            if (results == null)
            {
                Toast.MakeText(this, "Error retrieving locations from search api", ToastLength.Short).Show();
            }
        }

        private async void LvLocations_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            progressDialog.Show();
            var location = locationViewAdaptor[e.Position];
            var weatherForecast = await weatherService.GetWeatherForecastAsync(location.WoeId);

            if (weatherForecast == null)
            {
                progressDialog.Cancel();
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
            progressDialog.Cancel();
        }
    }
}


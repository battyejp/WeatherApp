using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;

namespace WeatherApp
{
    public class LocationManager
    {
        //TODO sort unhappy path out
        private FusedLocationProviderClient fusedLocationProviderClient;

        public LocationManager(Activity activity)
        {
            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(activity);
        }

        public bool IsGooglePlayServicesInstalled(Context context)
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);
            if (queryResult == ConnectionResult.Success)
            {
                //Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                //Log.Error("MainActivity", "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }

        public async Task<Location> GetLastLocationFromDevice()
        {
            Location location = await fusedLocationProviderClient.GetLastLocationAsync();

            if (location == null)
            {
                return null;
            }
            else
            {
                return location;
            }
        }

        public async Task RequestLocationUpdatesAsync(MainActivity mainActivity)
        {
            LocationRequest locationRequest = new LocationRequest()
                      .SetPriority(LocationRequest.PriorityHighAccuracy)
                      .SetInterval(60 * 1000 * 5)
                      .SetFastestInterval(60 * 1000 * 2);

            await fusedLocationProviderClient.RequestLocationUpdatesAsync(locationRequest, new FusedLocationProviderCallback(mainActivity));
        }
    }

    public class FusedLocationProviderCallback : LocationCallback
    {
        readonly MainActivity activity;

        public FusedLocationProviderCallback(MainActivity activity)
        {
            this.activity = activity;
        }

        public override void OnLocationAvailability(LocationAvailability locationAvailability)
        {
        }

        public override void OnLocationResult(LocationResult result)
        {
            if (result.Locations.Any())
            {
                var location = result.Locations.First();
            }
        }
    }
}
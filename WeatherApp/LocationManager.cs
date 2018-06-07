using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;
using Android.Support.V4.Content;

namespace WeatherApp
{
    public class LocationManager
    {
        private FusedLocationProviderClient fusedLocationProviderClient;

        public LocationManager(Activity activity)
        {
            if (!HasPermissions(activity) || !HasGooglePlayServicesInstalled(activity))
                return;

            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(activity);
        }

        public bool HasGooglePlayServicesInstalled(Context context)
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(context);
            return queryResult == ConnectionResult.Success;
        }


        public bool HasPermissions(Context context)
        {
            return ContextCompat.CheckSelfPermission(context, Manifest.Permission.AccessFineLocation) == Permission.Granted;
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
            if (!HasPermissions(mainActivity) || !HasGooglePlayServicesInstalled(mainActivity))
                return;

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
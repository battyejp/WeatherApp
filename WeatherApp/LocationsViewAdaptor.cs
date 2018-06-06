using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using WeatherApp.Common.Dtos;

namespace WeatherApp
{
    public class LocationsViewAdapter : BaseAdapter<LocationDto>
    {
        private List<LocationDto> locations;
        private Activity context;

        public LocationsViewAdapter(Activity context, List<LocationDto> locations)
            : base()
        {
            this.context = context;
            this.locations = locations;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override LocationDto this[int position]
        {
            get { return locations[position]; }
        }
        public override int Count
        {
            get { return locations.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = this[position].Title;
            return view;
        }
    }
}
using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using WeatherApp.Common.Dtos;
using WeatherApp.Common.Models;

namespace WeatherApp
{
    public class LocationsViewAdapter : BaseAdapter<Location>
    {
        private IList<Location> locations;
        private Activity context;

        public LocationsViewAdapter(Activity context, IList<Location> locations)
            : base()
        {
            this.context = context;
            this.locations = locations;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Location this[int position]
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

        public void Refresh(IList<Location> locations)
        {
            this.locations = locations;
            this.NotifyDataSetChanged();
        }
    }
}
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

namespace WeatherApp.Controls
{
    public class DailyWeather : LinearLayout
    {
        public DailyWeather(Context context)
            : base (context)
        {
            var myTextView = new TextView(this.Context)
            {
                Text = "wassup"
            };

            AddView(myTextView);
        }
    }
}
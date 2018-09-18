
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using WoMoDiary.Droid.Adapter;
using Java.Nio.Channels;
using WoMoDiary.Models;

namespace WoMoDiary.Droid.Fragments
{
    public class TripsFragment : Android.Support.V4.App.ListFragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            ListAdapter = new TripAdapter(Activity, new List<Trip>
            {
                new Trip{Name = "Östereich"},
                new Trip{Name = "Nordsee"},
                new Trip{Name = "Schweiz"},
                new Trip{Name = "Mecklenburg"}
            });
        }
    }
}

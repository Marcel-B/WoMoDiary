
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
using Android.Gms.Maps;

namespace WoMoDiary.Droid.Activities
{
    [Activity(Label = "SaveLocationActivity")]
    public class SaveLocationActivity : Activity
    {
        public MapFragment MyMapFragment { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LocationLayout);
            MyMapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapFragmentMyLocation);
            // Create your application here
        }
    }
}

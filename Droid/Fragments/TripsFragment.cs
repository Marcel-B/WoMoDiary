
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
using WoMoDiary.Domain;

namespace WoMoDiary.Droid.Fragments
{
    public class TripsFragment : Android.Support.V4.App.ListFragment
    {
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var store = ServiceLocator.Instance.Get<IDataStore<TripOtd>>();
            ListAdapter = new TripAdapter(Activity, (await store.GetItemsAsync()).ToList());
        }
    }
}

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
using WoMoDiary.Services;

namespace WoMoDiary.Android
{
    [Activity(Label = "PlaceDetailActivity")]
    public class PlaceDetailActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.placeDetailLayout);

            // Create your application here
            var name = FindViewById<TextView>(Resource.Id.textViewDetailPlaceName);
            var description = FindViewById<TextView>(Resource.Id.textViewDetailPlaceDescription);

            var localStore = AppStore.GetInstance();
            var place = localStore.CurrentPlace;
            name.Text = place.Name;
            description.Text = place.Description;
        }
    }
}
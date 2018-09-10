
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

namespace WoMoDiary.Droid.Fragments
{
    public class MapFragment : Android.Support.V4.App.Fragment
    {
        public static MapFragment NewInstance() =>
            new MapFragment { Arguments = new Bundle() };

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.fragment_map, container, false);
   

            return view;
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            //View view = inflater.Inflate(Resource.Layout.fragment_browse, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}

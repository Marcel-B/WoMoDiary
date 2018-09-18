using Android.OS;
using Android.Views;

namespace WoMoDiary.Droid.Fragments
{
    public class LocationFragment : Android.Support.V4.App.Fragment
    {

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var map = FragmentManager.FindFragmentById(Resource.Id.mapFragmentMyLocation);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.PhotoFragmentLayout, container, false);
            // Use this to return your custom view for this Fragment
            return view;
        }
    }
}


using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using WoMoDiary.Helpers;
using I18NPortable;
using Android.Gms.Maps;

namespace WoMoDiary.Droid.Fragments
{
    public class WoMoMapFragment : Fragment
    {
        public static WoMoMapFragment NewInstance() =>
            new WoMoMapFragment { Arguments = new Bundle() };

        MapFragment Map { get; set; }

        public Button SavePosition { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var foo = FragmentManager.FindFragmentById(Resource.Id.MyWomoMap);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_womomap, container, false);
            SavePosition = view.FindViewById<Button>(Resource.Id.ButtonSavePosition);
            SavePosition.Text = Strings.SAVE_POSITION.Translate();
            return view;
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            //View view = inflater.Inflate(Resource.Layout.fragment_browse, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}

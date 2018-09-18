using Android.OS;
using Android.Views;
using Android.Widget;
using WoMoDiary.Helpers;
using I18NPortable;

namespace WoMoDiary.Droid.Fragments
{
    public class PhotoFragment : Android.Support.V4.App.Fragment
    {
        public static PhotoFragment NewInstance() =>
            new PhotoFragment { Arguments = new Bundle() };

        public Button ShootButton { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var view = inflater.Inflate(Resource.Layout.PhotoFragmentLayout, container, false);
            ShootButton = view.FindViewById<Button>(Resource.Id.ButtonShoot);
            ShootButton.Text = Strings.SHOOT.Translate();
            return view;
        }
    }
}

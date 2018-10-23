
using Android.App;
using Android.OS;
using Android.Widget;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Helpers;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "EditPlaceActivity")]
    public class EditPlaceActivity : Activity
    {
        public EditPlaceActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<EditPlaceViewModel>();
        }

        public EditPlaceViewModel ViewModel { get; set; }

        public EditText Name { get; set; }
        public EditText Description { get; set; }
        public EditText Rating { get; set; }
        public Spinner PlaceTypes { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            GetViews();
        }

        private void GetViews()
        {
            Name = FindViewById<EditText>(Resource.Id.editTextEditPlaceName);
            Description = FindViewById<EditText>(Resource.Id.editTextEditPlaceDescription);
            Rating = FindViewById<EditText>(Resource.Id.editTextEditPlaceRating);
            PlaceTypes = FindViewById<Spinner>(Resource.Id.spinnerEditPlaceTypes);
        }
    }
}

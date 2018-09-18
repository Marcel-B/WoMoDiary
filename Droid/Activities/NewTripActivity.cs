
using Android.App;
using Android.OS;
using Android.Widget;

namespace WoMoDiary.Droid
{
    [Activity(Label = "NewTripActivity")]
    public class NewTripActivity : Activity
    {
        public Button AddTripButton { get; set; }
        public EditText EditTextDescription { get; set; }
        public EditText EditTextTripName { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewTripLayout);
            AddTripButton = FindViewById<Button>(Resource.Id.buttonAddNewTrip);
            EditTextTripName = FindViewById<EditText>(Resource.Id.editTextTripName);
            EditTextDescription = FindViewById<EditText>(Resource.Id.editTextDescription);

            AddTripButton.Click += (sender, args) =>
            {
                StartActivity(typeof(MainActivity));
            };
        }
    }
}

using Android.App;
using Android.OS;
using Android.Widget;
using WoMoDiary.Helpers;
using WoMoDiary.ViewModels;

namespace WoMoDiary.Android
{
    [Activity(Label = "NewTripActivity")]
    public class NewTripActivity : Activity
    {
        public NewTripViewModel ViewModel { get; set; }

        public NewTripActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewTripViewModel>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.newTripLayout);

            FindViewById<EditText>(Resource.Id.newTripNameEditText)
                .TextChanged += (sender, e) =>
                    ViewModel.TripName = e.Text.ToString();

            FindViewById<EditText>(Resource.Id.newTripDescriptionEditText)
                .TextChanged += (sender, e) =>
                    ViewModel.Description = e.Text.ToString();

            FindViewById<Button>(Resource.Id.saveNewTripButton)
                .Click += (sender, e) =>
                {
                    ViewModel.SaveTripCommand.Execute(null);
                    StartActivity(typeof(MainActivity));
                };
        }
    }
}

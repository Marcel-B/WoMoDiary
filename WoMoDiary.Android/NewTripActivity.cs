using Android.App;
using Android.OS;
using Android.Widget;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.ViewModels;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "NewTripActivity")]
    public class NewTripActivity : Activity
    {

        public NewTripActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<NewTripViewModel>();
        }

        public NewTripViewModel ViewModel { get; set; }
        public EditText EditTextTripName { get; set; }
        public EditText EditTextTripDescription { get; set; }
        public Button ButtonSaveTrip { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.newTripLayout);
            GetViews();
            SetControllStates();
            SetControllEvents();
            Localize();
        }

        private void GetViews()
        {
            EditTextTripName =
                FindViewById<EditText>(Resource.Id.newTripNameEditText);

            EditTextTripDescription =
                FindViewById<EditText>(Resource.Id.newTripDescriptionEditText);

            ButtonSaveTrip =
                FindViewById<Button>(Resource.Id.saveNewTripButton);
        }

        private void SetControllStates()
        {
            ButtonSaveTrip.Enabled = false;
        }

        private void SetControllEvents()
        {
            EditTextTripName.TextChanged += (sender, e) =>
            {
                ViewModel.TripName = e.Text.ToString();
                ButtonSaveTrip.Enabled = ViewModel.SaveTripCommand.CanExecute(null);
            };

            EditTextTripDescription.TextChanged += (sender, e) =>
            {
                ViewModel.Description = e.Text.ToString();
                ButtonSaveTrip.Enabled = ViewModel.SaveTripCommand.CanExecute(null);
            };

            ButtonSaveTrip.Click += (sender, e) =>
            {
                ViewModel.SaveTripCommand.Execute(null);
                StartActivity(typeof(TripActivity));
                Finish();
            };
        }

        private void Localize()
        {
            ButtonSaveTrip.Text = Strings.SAVE;
            EditTextTripName.Hint = Strings.ENTER_TRIP_NAME;
            EditTextTripDescription.Hint = Strings.ENTER_DESCRIPTION;
        }
    }
}

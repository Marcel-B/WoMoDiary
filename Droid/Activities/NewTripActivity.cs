
using Android.App;
using Android.OS;
using Android.Widget;
using WoMoDiary.ViewModels;

namespace WoMoDiary.Droid
{
    [Activity(Label = "NewTripActivity")]
    public class NewTripActivity : Activity
    {
        public Button AddTripButton { get; set; }
        public EditText EditTextDescription { get; set; }
        public EditText EditTextTripName { get; set; }
        private NewTripViewModel _viewModel;

        public NewTripActivity()
        {
            _viewModel = new NewTripViewModel();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewTripLayout);

            AddTripButton = FindViewById<Button>(Resource.Id.buttonAddNewTrip);
            EditTextTripName = FindViewById<EditText>(Resource.Id.editTextTripName);
            EditTextDescription = FindViewById<EditText>(Resource.Id.editTextDescription);

            AddTripButton.Click += (sender, args) =>
            {
                _viewModel.SaveTripCommand.Execute(null);
                StartActivity(typeof(MainActivity));
            };

            EditTextTripName.AfterTextChanged += (sender, e) =>
            {
                _viewModel.TripName = ((EditText)sender).Text;
                AddTripButton.Enabled = _viewModel.SaveTripCommand.CanExecute(null);
            };

            EditTextDescription.AfterTextChanged += (sender, e) =>
            {
                _viewModel.Description = ((EditText)sender).Text;
                AddTripButton.Enabled = _viewModel.SaveTripCommand.CanExecute(null);
            };
        }
    }
}

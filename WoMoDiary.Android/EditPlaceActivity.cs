
using Android.App;
using Android.OS;
using Android.Widget;
using com.b_velop.WoMoDiary.ViewModels;
using com.b_velop.WoMoDiary.Helpers;
using Android.Provider;
using com.b_velop.WoMoDiary.Meta;
using com.b_velop.WoMoDiary.Domain;
using Reces = Android.Resource;

namespace com.b_velop.WoMoDiary.Android
{
    [Activity(Label = "EditPlaceActivity")]
    public class EditPlaceActivity : Activity
    {
        public EditPlaceActivity()
        {
            ViewModel = ServiceLocator.Instance.Get<EditPlaceViewModel>();
            ViewModel.UpdateReady = PlaceUpdateReady;
        }

        public EditPlaceViewModel ViewModel { get; set; }

        public EditText Name { get; set; }
        public EditText Description { get; set; }
        public EditText Rating { get; set; }
        public Button EditPlaceSave { get; set; }
        public Spinner PlaceTypes { get; set; }

        private void PlaceUpdateReady(bool success)
        {
            if (success) Finish();
            else App.LogOutLn("Error while updating place", GetType().Name);
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.editPlaceLayout);

            // Create your application here
            ViewModel.FetchPlace();
            GetViews();
            Localize();
            InitViews();
            SetViewsEvents();

            var places = new Place[]
            {
                        new MotorhomePlace(),
                        new CampingPlace(),
                        new Hotel(),
                        new Poi(),
                        new Restaurant()
            };
            var adapter = new ArrayAdapter<Place>(this,
                Reces.Layout.SimpleSpinnerItem, places);

            PlaceTypes.Adapter = adapter;

        }

        private void GetViews()
        {
            Name = FindViewById<EditText>(Resource.Id.editTextEditPlaceName);
            Description = FindViewById<EditText>(Resource.Id.editTextEditPlaceDescription);
            Rating = FindViewById<EditText>(Resource.Id.editTextEditPlaceRating);
            PlaceTypes = FindViewById<Spinner>(Resource.Id.spinnerEditPlaceTypes);
            EditPlaceSave = FindViewById<Button>(Resource.Id.buttonEditPlaceSave);
        }
        private void Localize()
        {
            EditPlaceSave.Text = Strings.SAVE;
            Description.Hint = Strings.DESCRIPTION;
        }
        private void InitViews()
        {
            Name.Text = ViewModel.Name;
            Description.Text = ViewModel.Description;
            Rating.Text = ViewModel.Rating;
        }
        private void SetViewsEvents()
        {
            EditPlaceSave.Click += (sender, e) =>
            {
                ViewModel.SavePlaceCommand.Execute(null);
            };
            Name.TextChanged += (sender, e) =>
            {
                ViewModel.Name = e.Text.ToString();
            };
            Description.TextChanged += (sender, e) =>
            {
                ViewModel.Description = e.Text.ToString();
            };
            Rating.TextChanged += (sender, e) =>
            {
                ViewModel.Rating = e.Text.ToString();
            };
        }
    }
}

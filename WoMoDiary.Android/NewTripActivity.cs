using System;
using Android.App;
using Android.OS;
using Android.Widget;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;
using WoMoDiary.Services;

namespace WoMoDiary.Android
{
    [Activity(Label = "NewTripActivity")]
    public class NewTripActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (App.User == null)
                App.User = new User
                {
                    Id = Guid.Parse("569dd649-f9f8-4990-b31b-45d43dda82c2"),
                    Created = DateTimeOffset.Now
                };
            // Create your application here
            SetContentView(Resource.Layout.newTripLayout);
            var tripName = FindViewById<EditText>(Resource.Id.newTripNameEditText);
            var tripDescription = FindViewById<EditText>(Resource.Id.newTripDescriptionEditText);
            var saveButton = FindViewById<Button>(Resource.Id.saveNewTripButton);
            saveButton.Click += (object sender, EventArgs e) =>
            {
                var tripStore = ServiceLocator.Instance.Get<IDataStore<Trip>>();

                var trip =new Trip
                {
                    Id = Guid.NewGuid(),
                    Name = tripName.Text,
                    Description = tripDescription.Text,
                    Created = DateTimeOffset.Now,
                    User = App.User
                };
                tripStore.AddItemAsync(trip);
                var store = AppStore.GetInstance();
                store.Trips.Add(trip);
                System.Diagnostics.Debug.WriteLine($"New Trip '{tripName.Text}' Saved!");
                System.Diagnostics.Debug.WriteLine($"User Id '{trip.User.Name}' Saved!");
                StartActivity(typeof(MainActivity));
            };
        }
    }
}

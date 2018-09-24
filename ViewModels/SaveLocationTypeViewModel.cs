using System;
using WoMoDiary.Domain;
using WoMoDiary.Services;
using System.Collections.Generic;

namespace WoMoDiary.ViewModels
{
    public class SaveLocationTypeViewModel : BaseViewModel
    {
        public IPlace Place { get; set; }
        public Command SaveLocationTypeCommand { get; set; }

        public SaveLocationTypeViewModel()
        {
            SaveLocationTypeCommand = new Command(Execute, CanExecute);
        }

        private bool CanExecute(object arg)
            => true;

        private void Execute(object obj)
        {
            var store = AppStore.GetInstance();
            var location = store.CurrentLocation;
            var trip = store.CurrentTrip;
            Place.Name = location.Name;
            Place.Description = location.Description;
            Place.Id = Guid.NewGuid();
            Place.Location = location;
            trip.Places.Add((Place)Place);
            System.Diagnostics.Debug.WriteLine("Location saved");
        }
    }
}

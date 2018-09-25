﻿using WoMoDiary.Services;
using System;
using WoMoDiary.Domain;

namespace WoMoDiary.ViewModels
{
    public class SaveLocationViewModel : BaseViewModel
    {
        public SaveLocationViewModel()
        {
            SaveLocationCommand = new Command(SaveLocationExecute, CanExecuteSaveLocation);
        }

        private bool CanExecuteSaveLocation(object arg)
            => true;

        private void SaveLocationExecute(object obj)
        {
            System.Diagnostics.Debug.WriteLine($"Location Saved: Long {Longitude} - Lat {Latitude} - Alt {Altitude}");
            var store = AppStore.GetInstance();
            var place = new Place
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Description = Description,
                Longitude = Longitude,
                Latitude = Latitude,
                Created = DateTimeOffset.Now
            };
            store.CurrentPlace = place;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value);
        }

        private double _altitude;
        public double Altitude
        {
            get => _altitude;
            set => SetProperty(ref _altitude, value);
        }

        public Command SaveLocationCommand { get; set; }
    }
}

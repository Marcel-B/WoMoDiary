using System;
using System.Collections.Generic;
using System.Text;
using WoMoDiary.Domain;
using WoMoDiary.Helpers;

namespace WoMoDiary.ViewModels
{
    public class NewPlaceViewModel : BaseViewModel
    {
        public PlaceType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short Rating { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public Command SavePlaceCommand { get; set; }

        public NewPlaceViewModel()
        {
            SavePlaceCommand = new Command(ExecuteSave, CanExecuteSave);
        }

        private bool CanExecuteSave(object arg)
            => true;

        private void ExecuteSave(object obj)
        {
        }
    }
}

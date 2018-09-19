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

using System;

using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Helpers;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class EditPlaceViewModel : BaseViewModel
    {
        public EditPlaceViewModel()
        {
            SavePlaceCommand = new Command(ExecuteSavePlace, CanExecuteSavePlace);
        }

        public Action<bool> UpdateReady { get; set; }
        public void FetchPlace()
        {
            _place = AppStore.Instance.CurrentPlace;
            Name = _place.Name;
            Description = _place.Description;
            Rating = _place.Rating.ToString();
            SelectedPlaceType = _place.Type;
        }

        private bool CanExecuteSavePlace(object arg)
            => !string.IsNullOrWhiteSpace(Name);

        private async void ExecuteSavePlace(object obj)
        {
            _place.LastEdit = DateTimeOffset.Now;
            _place.Name = Name;
            _place.Description = Description;
            _place.Rating = short.Parse(Rating);
            _place.Type = SelectedPlaceType;
            var result = await PlaceStore.UpdateItemAsync(_place);
            if (result == null)
            {
                ErrorAction?.Invoke($"Update Place '{_place.Name}' failed:");
                App.LogOutLn($"Update Place '{_place.Name}' failed.");
                UpdateReady?.Invoke(false);
            }
            else
            {
                UpdateReady?.Invoke(true);
            }
        }

        private Place _place;

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

        private string _rating;
        public string Rating
        {
            get => _rating;
            set => SetProperty(ref _rating, value);
        }

        private PlaceType _selectedPlaceType;
        public PlaceType SelectedPlaceType
        {
            get => _selectedPlaceType;
            set => SetProperty(ref _selectedPlaceType, value);
        }
        public Command SavePlaceCommand { get; set; }
    }
}

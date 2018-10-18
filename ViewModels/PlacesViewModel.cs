using System.Collections.ObjectModel;
using com.b_velop.WoMoDiary.Domain;
using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class PlacesViewModel : BaseViewModel
    {
        public ObservableCollection<Place> Places { get; set; }

        public PlacesViewModel()
        {
            if (Places == null)
                Places = new ObservableCollection<Place>();
        }

        public void PullPlaces()
        {
            // Get Trips from Users collection
            Places.Clear();
            var places = AppStore.Instance.CurrentTrip.Places;

            foreach (var place in places)
                Places.Add(place);
        }
    }
}

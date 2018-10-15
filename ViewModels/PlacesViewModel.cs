using System;
using System.Collections.ObjectModel;
using WoMoDiary.Domain;
using System.Threading.Tasks;
using WoMoDiary.Services;
using System.Linq;

namespace WoMoDiary.ViewModels
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

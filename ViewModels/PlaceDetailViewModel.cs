using com.b_velop.WoMoDiary.Services;

namespace com.b_velop.WoMoDiary.ViewModels
{
    public class PlaceDetailViewModel : BaseViewModel
    {
        public PlaceDetailViewModel()
        {
            PullData();
        }

        public void PullData()
        {
            var place = AppStore.Instance.CurrentPlace;
            Name = place.Name;
            Description = place.Description;
            Longitude = place.Longitude;
            Latitude = place.Latitude;
            Altitude = place.Altitude;
            Rating = place.Rating;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public short Rating { get; set; }
    }
}

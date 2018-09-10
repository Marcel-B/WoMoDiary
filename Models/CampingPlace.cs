using System;

namespace WoMoDiary.Models
{
    public class CampingPlace
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }

        public Location CampingPlaceLocation { get; set; }
        public Rating CampingPlaceRating{ get; set; }

        public CampingPlace()
        {
            Created = DateTimeOffset.Now;
        }
    }
}

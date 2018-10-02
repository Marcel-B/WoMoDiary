using System;
using System.ComponentModel.DataAnnotations;

namespace WoMoDiary.Domain
{
    public class Place
    {
        public Place()
        {
            Created = DateTimeOffset.Now;
        }

        public Guid PlaceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AssetName { get; set; }

        public short Rating { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Altitude { get; set; }

        public PlaceType Type { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastEdit { get; set; }

        public Trip TripId { get; set; }

        public Trip Trip { get; set; }
    }
}

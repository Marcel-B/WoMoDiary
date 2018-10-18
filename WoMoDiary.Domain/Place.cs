using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace com.b_velop.WoMoDiary.Domain
{
    public class Place : IItem
    {
        [NotMapped]
        [JsonIgnoreAttribute]
        public Guid Id { get => PlaceId; set => PlaceId = value; }

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

        public Guid TripId { get; set; }

        [JsonIgnoreAttribute]
        public Trip Trip { get; set; }
    }
}

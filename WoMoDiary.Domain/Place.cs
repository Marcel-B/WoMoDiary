using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WoMoDiary.Domain
{
    public class Place : IItem
    {
        public Place()
        {
            Created = DateTimeOffset.Now;
        }

        [JsonProperty("id")]
        [Key]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("rating")]
        public short Rating { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("altitude")]
        public double Altitude { get; set; }

        [JsonProperty("type")]
        public PlaceType Type { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("lastEdit")]
        public DateTimeOffset LastEdit { get; set; }

        [JsonProperty("trip")]
        public Trip Trip { get; set; }
    }
}

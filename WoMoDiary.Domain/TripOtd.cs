// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WoMoDiary.Domain;
//
//    var tripOtd = TripOtd.FromJson(jsonString);

namespace WoMoDiary.Domain
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TripOtd
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("lastEdit")]
        public DateTimeOffset LastEdit { get; set; }

        [JsonProperty("places")]
        public List<Place> Places { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        public User User { get; set; }

    }

    public partial class Place : IPlace
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        public Guid Trip { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? Created { get; set; }

        [JsonProperty("lastEdit", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastEdit { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("assetName")]
        public string AssetName { get; set; }

        [JsonProperty("isGood")]
        public bool? IsGood { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("altitude")]
        public double Altitude { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("lastEdit")]
        public DateTimeOffset LastEdit { get; set; }
    }

    public partial class TripOtd
    {
        public static List<TripOtd> FromJson(string json) => JsonConvert.DeserializeObject<List<TripOtd>>(json, WoMoDiary.Domain.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<TripOtd> self) => JsonConvert.SerializeObject(self, WoMoDiary.Domain.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

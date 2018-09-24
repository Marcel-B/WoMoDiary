using System;
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WoMoDiary.ModelHelper
{
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using com.marcelbenders;
    //
    //    var hyperTrip = HyperTrip.FromJson(jsonString);



    public partial class HyperTrip
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
    }

    public partial class Place
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

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
        public object IsGood { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("longitude")]
        public long Longitude { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("altitude")]
        public long Altitude { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("lastEdit")]
        public DateTimeOffset LastEdit { get; set; }
    }

    public partial class HyperTrip
    {
        public static List<HyperTrip> FromJson(string json) => JsonConvert.DeserializeObject<List<HyperTrip>>(json, com.marcelbenders.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<HyperTrip> self) => JsonConvert.SerializeObject(self, com.marcelbenders.Converter.Settings);
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


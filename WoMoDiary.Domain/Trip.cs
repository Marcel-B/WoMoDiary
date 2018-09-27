﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace WoMoDiary.Domain
{
    public class Trip : IItem
    {
        [JsonProperty("id")]
        [Key]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("lastEdit")]
        public DateTimeOffset? LastEdit { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("places")]
        public List<Place> Places { get; set; }
    }
}

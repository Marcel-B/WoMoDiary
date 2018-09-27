using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WoMoDiary.Domain
{
    public class User : IItem
    {
        public User()
        {
            Created = DateTimeOffset.Now;
        }

        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Hash")]
        public byte[] Hash { get; set; }

        [JsonProperty("Salt")]
        public byte[] Salt { get; set; }

        [JsonProperty("Created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("LastEdit")]
        public DateTimeOffset? LastEdit { get; set; }

        [JsonProperty("Trips")]
        public IEnumerable<Trip> Trips { get; set; }
    }
}

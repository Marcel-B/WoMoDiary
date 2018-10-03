using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace WoMoDiary.Domain
{
    public class Trip
    {
        public Trip()
        {
            Created = DateTimeOffset.Now;
            Places = new List<Place>();
        }

        public Guid TripId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? LastEdit { get; set; }

        public Guid UserId { get; set; }


        [JsonIgnoreAttribute]
        public User User { get; set; }

        public List<Place> Places { get; set; }
    }
}

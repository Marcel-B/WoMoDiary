using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace com.b_velop.WoMoDiary.Domain
{
    public class Trip : IItem
    {
        [NotMapped]
        [JsonIgnoreAttribute]
        public Guid Id { get => TripId; set => TripId = value; }

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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WoMoDiary.Domain
{
    public class User : IItem
    {

        [NotMapped]
        [JsonIgnoreAttribute]
        public Guid Id { get => UserId; set => UserId = value; }

        public User()
        {
            Created = DateTimeOffset.Now;
            Trips = new List<Trip>();
        }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset LastEdit { get; set; }

        public List<Trip> Trips { get; set; }
    }
}

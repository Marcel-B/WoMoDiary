using System;
namespace WoMoDiary.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public int Height { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }

        public Location(){}
    }
}

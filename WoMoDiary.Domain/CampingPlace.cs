using System;
namespace WoMoDiary.Domain
{
    public class CampingPlace : IPlace
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }

        public Location Location { get; set; }
        //public Rating Rating { get; set; }
        public string AssetName { get; set; }
        public bool? IsGood { get; set; }

        public CampingPlace()
        {
            Name = "Camping place";
            AssetName = "Camping";
            Created = DateTimeOffset.Now;
        }
    }

    public class Stellpatz : IPlace
    {
        public Stellpatz()
        {
            Name = "Stellplatz";
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public string AssetName { get; set; }
        public bool? IsGood { get; set; }
    }

    public class Restaurant : IPlace
    {
        public Restaurant()
        {
            Name = "Restaurant";
            AssetName = "Restaurant";
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public string AssetName { get; set; }
        public bool? IsGood { get; set; }
    }

    public class NicePlace : IPlace
    {
        public NicePlace()
        {
            Name = "Nice place";
            AssetName = "SightSeeing";
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public string AssetName { get; set; }
        public bool? IsGood { get; set; }
    }

    public class Hotel : IPlace
    {
        public Hotel()
        {
            Name = "Hotel";
            AssetName = "Hotel";
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public string AssetName { get; set; }
        public bool? IsGood { get; set; }
    }
}

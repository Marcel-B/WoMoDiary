using System;
namespace WoMoDiary.Domain
{
    //public class Place : IPlace
    //{
        //public Guid Id { get; set; }

        //public string Name { get; set; }
        //public string Description { get; set; }

        //public DateTimeOffset Created { get; set; }
        //public DateTimeOffset LastEdit { get; set; }

        //public Location Location { get; set; }
        ////public Rating Rating { get; set; }
        //public string AssetName { get; set; }
        //public bool? IsGood { get; set; }

        //public Place()
        //{
        //    //Name = "Camping place";
        //    //AssetName = "Camping";
        //    Created = DateTimeOffset.Now;
        //}
    //}

    public class CampingPlace : Place
    {
        //public Guid Id { get; set; }

        //public string Name { get; set; }
        //public string Description { get; set; }

        //public DateTimeOffset Created { get; set; }
        //public DateTimeOffset LastEdit { get; set; }

        //public Location Location { get; set; }
        ////public Rating Rating { get; set; }
        //public string AssetName { get; set; }
        //public bool? IsGood { get; set; }

        public CampingPlace()
        {
            Name = "Camping place";
            AssetName = "Camping";
            Created = DateTimeOffset.Now;
        }
    }

    public class Stellpatz : Place
    {
        public Stellpatz()
        {
            Name = "Stellplatz";
        }
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public Location Location { get; set; }
        //public string AssetName { get; set; }
        //public bool? IsGood { get; set; }
    }

    public class Restaurant : Place
    {
        public Restaurant()
        {
            Name = "Restaurant";
            AssetName = "Restaurant";
        }

        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public Location Location { get; set; }
        //public string AssetName { get; set; }
        //public bool? IsGood { get; set; }
    }

    public class NicePlace : Place
    {
        public NicePlace()
        {
            Name = "Nice place";
            AssetName = "SightSeeing";
        }
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public Location Location { get; set; }
        //public string AssetName { get; set; }
        //public bool? IsGood { get; set; }
    }

    public class Hotel : Place
    {
        public Hotel()
        {
            Name = "Hotel";
            AssetName = "Hotel";
        }
        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public Location Location { get; set; }
        //public string AssetName { get; set; }
        //public bool? IsGood { get; set; }
    }
}

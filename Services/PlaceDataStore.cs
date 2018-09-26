using System;
using WoMoDiary.Domain;

namespace WoMoDiary.Services
{
    public class PlaceDataStore : CloudDataStore<Place>
    {
        protected override string Route
        {
            get => $"api/place/";
            set => Route = value;
        }
    }
}

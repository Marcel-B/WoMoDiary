using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.Repository
{
    public interface IRepository
    {
        Task<IList<Trip>> GetTripsByUser(Guid userId);


    }
    public class TripRepositoryMock : IRepository
    {
        //public async Task<IList<Trip>> GetTripsByUser(Guid userId)
        //{

        //}
        public Task<IList<Trip>> GetTripsByUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
    //public class TripRepository : IRepository
    //{
    //    public async Task<IList<Trip>> GetTripsByUser(Guid userId)
    //    {
    //        var http = new HttpClient();

    //    }
    //}
}

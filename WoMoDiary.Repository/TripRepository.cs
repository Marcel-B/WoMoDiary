using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using WoMoDiary.Domain;
using System.Net.Http;

namespace WoMoDiary.Repository
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

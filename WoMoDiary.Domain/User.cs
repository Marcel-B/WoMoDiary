using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace WoMoDiary.Domain
{
    public class User : IItem
    {
        public User()
        {
            Created = DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? LastEdit { get; set; }
        public IEnumerable<Trip> Trips { get; set; }

    }
}

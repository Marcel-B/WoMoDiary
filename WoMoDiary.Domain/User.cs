using System;
namespace WoMoDiary.Domain
{
    public class User
    {
        public User()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? LastEdit { get; set; }
    }
}

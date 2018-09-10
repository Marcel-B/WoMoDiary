using System;
namespace WoMoDiary.Models
{
    public class Rating
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }

        public short Points { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Rating()
        {
        }
    }
}

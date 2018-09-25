using System;
using System.Collections.Generic;
namespace WoMoDiary.Domain
{
    public class Trip : IItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? LastEdit { get; set; }
        public User User { get; set; }
        public List<Place> Places { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace WoMoDiary.Domain
{
    public class Trip
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastEdit { get; set; }

        public IList<IPlace> Places { get; set; }
        public IList<ITag> Tags { get; set; }

        public Trip()
        {
            Created = DateTimeOffset.Now;
            Places = new List<IPlace>();
            Tags = new List<ITag>();
        }
    }
}

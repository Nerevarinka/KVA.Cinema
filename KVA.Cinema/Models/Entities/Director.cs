namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Director
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public Director()
        {
            Videos = new HashSet<Video>();
        }
    }
}

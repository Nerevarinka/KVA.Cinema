﻿namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Country
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Video> Videos { get; set; }

        public Country()
        {
            Videos = new HashSet<Video>();
        }
    }
}

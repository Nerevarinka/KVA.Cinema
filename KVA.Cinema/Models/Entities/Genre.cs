namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Genre
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<VideoGenre> VideoGenres { get; set; }

        public Genre()
        {
            VideoGenres = new HashSet<VideoGenre>();
        }
    }
}

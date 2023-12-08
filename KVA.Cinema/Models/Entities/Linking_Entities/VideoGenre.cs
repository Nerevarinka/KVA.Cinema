namespace KVA.Cinema.Models.Entities
{
    using System;

    public class VideoGenre
    {
        public Guid Id { get; set; }

        public Guid GenreId { get; set; }

        public Guid VideoId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Video Video { get; set; }
    }
}

namespace KVA.Cinema.Models.Entities
{
    using System;

    public class Review
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid VideoId { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Video Video { get; set; }
    }
}

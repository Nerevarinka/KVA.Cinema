namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Subscription
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public Guid LevelId { get; set; }

        public DateTime ReleasedIn { get; set; }

        public int Duration { get; set; }

        public DateTime AvailableUntil { get; set; }

        public virtual SubscriptionLevel Level { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<VideoInSubscription> VideoInSubscriptions { get; set; }

        public Subscription()
        {
            Tags = new HashSet<Tag>();
            Users = new HashSet<User>();
            VideoInSubscriptions = new HashSet<VideoInSubscription>();
        }
    }
}

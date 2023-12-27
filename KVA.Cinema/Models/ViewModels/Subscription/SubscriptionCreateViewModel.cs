namespace KVA.Cinema.Models.Subscription
{
    using System;

    public class SubscriptionCreateViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public Entities.SubscriptionLevel Level { get; set; }

        public DateTime ReleasedIn { get; set; }

        public int Duration { get; set; }

        public DateTime AvailableUntil { get; set; }
    }
}

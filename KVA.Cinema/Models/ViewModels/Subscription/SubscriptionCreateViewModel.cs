namespace KVA.Cinema.Models.Subscription
{
    using System;

    internal struct SubscriptionCreateViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string Level { get; set; }

        public DateTime ReleasedIn { get; set; }

        public int Duration { get; set; }

        public DateTime AvailableUntil { get; set; }

        public SubscriptionCreateViewModel(string title, string description, decimal cost, string level, DateTime releasedIn, int duration, DateTime availableUntil)
        {
            Title = title;
            Description = description;
            Cost = cost;
            Level = level;
            ReleasedIn = releasedIn;
            Duration = duration;
            AvailableUntil = availableUntil;
        }
    }
}

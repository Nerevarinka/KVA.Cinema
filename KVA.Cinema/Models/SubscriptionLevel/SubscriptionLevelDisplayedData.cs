namespace KVA.Cinema.Models.SubscriptionLevel
{
    using System;

    internal struct SubscriptionLevelDisplayedData
    {
        public Guid Id { get; set; }

        public string Title { get; }

        public SubscriptionLevelDisplayedData(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}

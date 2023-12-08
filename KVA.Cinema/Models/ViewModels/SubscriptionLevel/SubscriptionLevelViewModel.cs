namespace KVA.Cinema.Models.SubscriptionLevel
{
    using System;

    internal struct SubscriptionLevelViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; }

        public SubscriptionLevelViewModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}

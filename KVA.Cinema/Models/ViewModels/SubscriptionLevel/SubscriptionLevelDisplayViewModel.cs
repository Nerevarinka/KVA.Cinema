namespace KVA.Cinema.Models.SubscriptionLevel
{
    using System;

    internal class SubscriptionLevelDisplayViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; }

        public SubscriptionLevelDisplayViewModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}

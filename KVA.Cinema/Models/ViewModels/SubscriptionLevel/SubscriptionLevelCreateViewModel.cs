namespace KVA.Cinema.Models.SubscriptionLevel
{
    using System;

    internal struct SubscriptionLevelCreateViewModel
    {
        public string Title { get; }

        public SubscriptionLevelCreateViewModel(string title)
        {
            Title = title;
        }
    }
}

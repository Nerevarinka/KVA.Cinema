namespace KVA.Cinema.Models.SubscriptionLevel
{
    using System;

    internal class SubscriptionLevelCreateViewModel
    {
        public string Title { get; }

        public SubscriptionLevelCreateViewModel(string title)
        {
            Title = title;
        }
    }
}

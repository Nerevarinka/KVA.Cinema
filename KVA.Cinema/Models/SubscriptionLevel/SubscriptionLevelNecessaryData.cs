namespace KVA.Cinema.Models.SubscriptionLevel
{
    using System;

    internal struct SubscriptionLevelNecessaryData
    {
        public string Title { get; }

        public SubscriptionLevelNecessaryData(string title)
        {
            Title = title;
        }
    }
}

namespace KVA.Cinema.Models.ViewModels.Subscription
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubscriptionEditViewModel
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

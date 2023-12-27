namespace KVA.Cinema.Models.ViewModels.UserSubscription
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserSubscriptionEditViewModel
    {
        public Guid Id { get; set; }

        public Guid SubscriptionId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ActivatedOn { get; set; }

        public DateTime LastUntil { get; set; }
    }
}

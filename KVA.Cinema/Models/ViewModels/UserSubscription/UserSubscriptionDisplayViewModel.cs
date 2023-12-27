namespace KVA.Cinema.Models.UserSubscription
{
    using System;

    internal class UserSubscriptionDisplayViewModel
    {
        public Guid Id { get; set; }

        public Guid SubscriptionId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ActivatedOn { get; set; }

        public DateTime LastUntil { get; set; }
    }
}

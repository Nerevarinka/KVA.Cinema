namespace KVA.Cinema.Models.UserSubscription
{
    using System;

    internal class UserSubscriptionCreateViewModel
    {
        public Guid Id { get; set; }

        public Guid SubscriptionId { get; set; }

        public Guid UserId { get; set; }

        public DateTime ActivatedOn { get; set; }

        public DateTime LastUntil { get; set; }

        public UserSubscriptionCreateViewModel(Guid id, Guid subscriptionId, Guid userId, DateTime activatedOn, DateTime lastUntil)
        {
            Id = id;
            SubscriptionId = subscriptionId;
            UserId = userId;
            ActivatedOn = activatedOn;
            LastUntil = lastUntil;
        }
    }
}

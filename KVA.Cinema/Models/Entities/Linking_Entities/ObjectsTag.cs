namespace KVA.Cinema.Models.Entities
{
    using System;

    public class ObjectsTag
    {
        public Guid Id { get; set; }

        public Guid TagId { get; set; }

        public Guid? VideoId { get; set; }

        public Guid? SubscriptionId { get; set; }

        public virtual Subscription Subscription { get; set; }

        public virtual Tag Tag { get; set; }

        public virtual Video Video { get; set; }
    }
}

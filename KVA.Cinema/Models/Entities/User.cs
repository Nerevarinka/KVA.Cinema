namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public DateTime? LastVisit { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<CommentMark> CommentMarks { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<UserSubscription> UserSubscriptions { get; set; }

        public virtual ICollection<VideoRate> VideoRates { get; set; }

        public User()
        {
            Comments = new HashSet<Comment>();
            CommentMarks = new HashSet<CommentMark>();
            Reviews = new HashSet<Review>();
            UserSubscriptions = new HashSet<UserSubscription>();
            VideoRates = new HashSet<VideoRate>();
        }
    }
}

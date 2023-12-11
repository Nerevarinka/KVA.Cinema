namespace KVA.Cinema.Models
{
    using KVA.Cinema.Models.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CinemaContext : DbContext
    {
        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<CommentMark> CommentMarks { get; set; }

        public virtual DbSet<Entities.Country> Countries { get; set; }

        public virtual DbSet<Entities.Director> Directors { get; set; }

        public virtual DbSet<Frame> Frames { get; set; }

        public virtual DbSet<Entities.Genre> Genres { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<Entities.SubscriptionLevel> SubscriptionLevels { get; set; }

        public virtual DbSet<ObjectsTag> ObjectsTags { get; set; }

        public virtual DbSet<Pegi> Pegis { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Entities.Subscription> Subscriptions { get; set; }

        public virtual DbSet<Subtitle> Subtitles { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<Entities.User> Users { get; set; }

        public virtual DbSet<Entities.UserSubscription> UserSubscriptions { get; set; }

        public virtual DbSet<Video> Videos { get; set; }

        public virtual DbSet<VideoGenre> VideoGenres { get; set; }

        public virtual DbSet<VideoInSubscription> VideoInSubscriptions { get; set; }

        public virtual DbSet<VideoRate> VideoRates { get; set; }

        public CinemaContext()
            : base()
        {
        }
    }
}

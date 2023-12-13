﻿namespace KVA.Cinema.Models.Entities
{
    using System;
    using System.Collections.Generic;

    public class Video
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Length { get; set; }

        public Guid CountryId { get; set; }

        public DateTime ReleasedIn { get; set; }

        public int Views { get; set; }

        public string Preview { get; set; }

        public Guid PegiId { get; set; }

        public Guid LanguageId { get; set; }

        public Guid DirectorId { get; set; }

        //public Guid? NextPartId { get; set; }

        //public Guid? PreviousPartId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Country Country { get; set; }

        public virtual Director Director { get; set; }

        public virtual ICollection<Frame> Frames { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<ObjectsTag> ObjectsTags { get; set; }

        public virtual Pegi Pegi { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Subtitle> Subtitles { get; set; }

        //public virtual ICollection<Video> Video1 { get; set; } //следующие видео

        //public virtual Video NextPart { get; set; }

        //public virtual Video PreviousPart { get; set; }

        public virtual ICollection<VideoGenre> VideoGenres { get; set; }

        public virtual ICollection<VideoInSubscription> VideoInSubscriptions { get; set; }

        public virtual ICollection<VideoRate> VideoRates { get; set; }

        public Video()
        {
            Comments = new HashSet<Comment>();
            Frames = new HashSet<Frame>();
            ObjectsTags = new HashSet<ObjectsTag>();
            Reviews = new HashSet<Review>();
            Subtitles = new HashSet<Subtitle>();
            //Video1 = new HashSet<Video>();
            VideoGenres = new HashSet<VideoGenre>();
            VideoInSubscriptions = new HashSet<VideoInSubscription>();
            VideoRates = new HashSet<VideoRate>();
        }
    }
}

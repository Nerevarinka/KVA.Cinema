﻿namespace KVA.Cinema.Models.ViewModels.Subscription
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using KVA.Cinema.Models.Entities;

    public class SubscriptionDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Cost ($)")]
        public decimal Cost { get; set; }

        [Display(Name = "Level Id")]
        public Guid LevelId { get; set; }

        [Display(Name = "Level")]
        public string LevelName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Released in")]
        public DateTime ReleasedIn { get; set; }

        [Display(Name = "Duration (days)")]
        public int Duration { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Available to buy until")]
        public DateTime AvailableUntil { get; set; }

        public bool IsPurchasedByCurrentUser { get; set; }

        [Display(Name = "Videos")]
        public IEnumerable<VideoInSubscription> VideosInSubscription { get; set; }

        [Display(Name = "Videos")]
        public IEnumerable<string> VideoNames { get; set; }

        [Display(Name = "Available videos")]
        public string VideoNamesInOneString => VideoNames == null ? string.Empty : string.Join(", ", VideoNames);
    }
}

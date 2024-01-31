namespace KVA.Cinema.Models.ViewModels.SubscriptionLevel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SubscriptionLevelDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}

namespace KVA.Cinema.Models.Genre
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GenreDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}

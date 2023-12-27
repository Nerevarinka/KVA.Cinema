namespace KVA.Cinema.Models.Genre
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GenreCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Title length must be in 2-20 symbols")]
        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}

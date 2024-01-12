namespace KVA.Cinema.Models.ViewModels.Genre
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class GenreEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(50, ErrorMessage = "Title length cannot be more than 128 symbols")]
        [MinLength(2, ErrorMessage = "Title length cannot be less than 2 symbols")]
        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}

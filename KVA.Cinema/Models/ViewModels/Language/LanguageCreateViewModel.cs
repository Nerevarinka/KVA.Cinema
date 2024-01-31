namespace KVA.Cinema.Models.ViewModels.Language
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguageCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(128, ErrorMessage = "Name length cannot be more than 128 symbols")]
        [MinLength(2, ErrorMessage = "Name length cannot be less than 2 symbols")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}

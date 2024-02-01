namespace KVA.Cinema.Models.ViewModels.Tag
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class TagCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, ErrorMessage = "Tag text length cannot be more than 20 symbols")]
        [MinLength(2, ErrorMessage = "Tag text length cannot be less than 2 symbols")]
        [Display(Name = "Tag text")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}

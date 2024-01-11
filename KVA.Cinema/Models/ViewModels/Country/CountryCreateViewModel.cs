namespace KVA.Cinema.Models.Country
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CountryCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name length must be in 2-20 symbols")]
        [Display(Name = "Last name")]
        public string Name { get; set; }
    }
}

namespace KVA.Cinema.Models.ViewModels.Pegi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class PegiCreateViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(0, 99, ErrorMessage = "Value is not valid for age restriction")]
        [Display(Name = "Age restriction")]
        public byte Type { get; set; }
    }
}

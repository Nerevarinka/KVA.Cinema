namespace KVA.Cinema.Models.ViewModels.Tag
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class TagDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Tag text")]
        public string Text { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}

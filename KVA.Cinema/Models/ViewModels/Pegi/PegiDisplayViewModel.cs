namespace KVA.Cinema.Models.ViewModels.Pegi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class PegiDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Age restriction")]
        public byte Type { get; set; }
    }
}

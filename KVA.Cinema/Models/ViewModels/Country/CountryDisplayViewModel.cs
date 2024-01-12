namespace KVA.Cinema.Models.Country
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CountryDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}

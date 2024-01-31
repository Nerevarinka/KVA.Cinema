namespace KVA.Cinema.Models.Director
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DirectorDisplayViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}

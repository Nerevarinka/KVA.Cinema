﻿namespace KVA.Cinema.Models.ViewModels.User
{
    using KVA.Cinema.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, ErrorMessage = "Last name length cannot be more than 20 symbols")]
        [MinLength(2, ErrorMessage = "Last name length cannot be less than 2 symbols")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, ErrorMessage = "First name length cannot be more than 20 symbols")]
        [MinLength(2, ErrorMessage = "First name length cannot be less than 2 symbols")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, ErrorMessage = "Nickname length cannot be more than 20 symbols")]
        [MinLength(2, ErrorMessage = "Nickname length cannot be less than 2 symbols")]
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date)]
        [ValidAge(14, 120, ErrorMessage = "Age must be in 14-120")]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Incorrect email format")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Subscriptions")]
        public IEnumerable<Entities.Subscription> Subscriptions { get; set; }
    }
}

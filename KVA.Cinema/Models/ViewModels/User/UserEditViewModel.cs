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
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name length must be in 2-20 symbols")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name length must be in 2-20 symbols")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Nickname length must be in 2-20 symbols")]
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
    }
}

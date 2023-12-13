namespace KVA.Cinema.Models.User
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using KVA.Cinema.Attributes;

    /// <summary>
    /// Essential data to create person (written by user)
    /// </summary>
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name length must be in 2-20 symbols")]
        public string LastName { get; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name length must be in 2-20 symbols")]
        public string FirstName { get; }

        [Required(ErrorMessage = "Required field")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Nickname length must be in 2-20 symbols")]
        public string Nickname { get; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date)]
        [ValidAge(14, 120, ErrorMessage = "Age must be in 14-120")]
        public DateTime BirthDate { get; }

        [Required(ErrorMessage = "Required field")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Incorrect email format")]
        public string Email { get; }

        public UserCreateViewModel(string lastName, string firstName, string nickname, DateTime birthDate, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            BirthDate = birthDate;
            Email = email;
        }
    }
}

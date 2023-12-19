namespace KVA.Cinema.Models.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Person's data to display on console
    /// </summary>
    public class UserDisplayViewModel
    {
        public Guid Id { get; set; }

        public string LastName { get; }

        public string FirstName { get; }

        public string Nickname { get; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; }

        public string Email { get; }

        public UserDisplayViewModel(Guid id, string lastName, string firstName, string nickname, DateTime birthDate, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Nickname = nickname;
            BirthDate = birthDate;
            Email = email;
        }
    }
}

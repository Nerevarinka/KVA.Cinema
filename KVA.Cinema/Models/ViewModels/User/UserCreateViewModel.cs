namespace KVA.Cinema.Models.User
{
    using System;

    /// <summary>
    /// Essential data to create person (written by user)
    /// </summary>
    internal class UserCreateViewModel
    {
        public string LastName { get; }

        public string FirstName { get; }

        public string Nickname { get; }

        public DateTime BirthDate { get; }

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

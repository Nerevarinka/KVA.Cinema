namespace KVA.Cinema.Models.User
{
    using System;

    /// <summary>
    /// Person's data to display on console
    /// </summary>
    internal struct UserDisplayedData
    {
        public Guid Id { get; set; }

        public string LastName { get; }

        public string FirstName { get; }

        public string Nickname { get; }

        public DateTime BirthDate { get; }

        public string Email { get; }

        public UserDisplayedData(Guid id, string lastName, string firstName, string nickname, DateTime birthDate, string email)
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

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

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Nickname { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string Email { get; set; }
    }
}

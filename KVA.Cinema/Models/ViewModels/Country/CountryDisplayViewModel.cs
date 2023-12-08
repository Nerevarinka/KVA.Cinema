namespace KVA.Cinema.Models.Country
{
    using System;

    internal class CountryDisplayViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public CountryDisplayViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

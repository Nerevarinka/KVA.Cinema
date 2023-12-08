namespace KVA.Cinema.Models.Country
{
    using System;

    internal class CountryCreateViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public CountryCreateViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

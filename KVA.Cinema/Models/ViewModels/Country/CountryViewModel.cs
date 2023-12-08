namespace KVA.Cinema.Models.Country
{
    using System;

    internal struct CountryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public CountryViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

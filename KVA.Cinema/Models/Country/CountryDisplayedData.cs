namespace KVA.Cinema.Models.Country
{
    using System;

    internal struct CountryDisplayedData
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public CountryDisplayedData(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

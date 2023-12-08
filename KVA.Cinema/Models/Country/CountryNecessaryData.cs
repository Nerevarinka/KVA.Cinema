namespace KVA.Cinema.Models.Country
{
    using System;

    internal struct CountryNecessaryData
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public CountryNecessaryData(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

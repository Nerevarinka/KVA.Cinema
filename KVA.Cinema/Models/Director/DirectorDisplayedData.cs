namespace KVA.Cinema.Models.Director
{
    using System;

    internal struct DirectorDisplayedData
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public DirectorDisplayedData(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

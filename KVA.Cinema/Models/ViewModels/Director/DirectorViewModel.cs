namespace KVA.Cinema.Models.Director
{
    using System;

    internal struct DirectorViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public DirectorViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

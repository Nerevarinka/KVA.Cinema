namespace KVA.Cinema.Models.Director
{
    using System;

    internal class DirectorDisplayViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; }

        public DirectorDisplayViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

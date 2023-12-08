namespace KVA.Cinema.Models.Director
{
    using System;

    internal struct DirectorCreateViewModel
    {
        public string Name { get; }

        public DirectorCreateViewModel(string name)
        {
            Name = name;
        }
    }
}

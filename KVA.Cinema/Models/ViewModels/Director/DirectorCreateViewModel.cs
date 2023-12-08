namespace KVA.Cinema.Models.Director
{
    using System;

    internal class DirectorCreateViewModel
    {
        public string Name { get; }

        public DirectorCreateViewModel(string name)
        {
            Name = name;
        }
    }
}

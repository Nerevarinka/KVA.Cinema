namespace KVA.Cinema.Models.Director
{
    using System;

    internal struct DirectorNecessaryData
    {
        public string Name { get; }

        public DirectorNecessaryData(string name)
        {
            Name = name;
        }
    }
}

namespace KVA.Cinema.Models.Genre
{
    using System;

    internal struct GenreNecessaryData
    {
        public string Title { get; }

        public GenreNecessaryData(string title)
        {
            Title = title;
        }
    }
}

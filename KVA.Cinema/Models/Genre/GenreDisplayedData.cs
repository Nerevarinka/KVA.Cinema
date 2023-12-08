namespace KVA.Cinema.Models.Genre
{
    using System;

    internal struct GenreDisplayedData
    {
        public Guid Id { get; set; }

        public string Title { get; }

        public GenreDisplayedData(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}

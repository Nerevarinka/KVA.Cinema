namespace KVA.Cinema.Models.Genre
{
    using System;

    internal class GenreDisplayViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; }

        public GenreDisplayViewModel(Guid id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}

namespace KVA.Cinema.Models.Genre
{
    using System;

    internal class GenreCreateViewModel
    {
        public string Title { get; }

        public GenreCreateViewModel(string title)
        {
            Title = title;
        }
    }
}

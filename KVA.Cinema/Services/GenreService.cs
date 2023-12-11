namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.Genre;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class GenreService : IService<GenreCreateViewModel, GenreDisplayViewModel>
    {
        public void Create(GenreCreateViewModel genreData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(genreData.Title))
            {
                throw new ArgumentNullException("Title has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                if (db.Genres.FirstOrDefault(x => x.Title == genreData.Title) != default)
                {
                    throw new DuplicatedEntityException($"Genre with title \"{genreData.Title}\" is already exist");
                }
            }

            Genre newGenre = new Genre()
            {
                Id = Guid.NewGuid(),
                Title = genreData.Title
            };

            using (CinemaContext db = new CinemaContext())
            {
                db.Genres.Add(newGenre);
                db.SaveChanges();
            }
        }

        public void Delete(Guid genreId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(genreId))
            {
                throw new ArgumentNullException("Genre Id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Genre genre = db.Genres.FirstOrDefault(x => x.Id == genreId);

                if (genre == default)
                {
                    throw new EntityNotFoundException($"Genre with Id \"{genreId}\" not found");
                }

                db.Genres.Remove(genre);
                db.SaveChanges();
            }
        }

        public IEnumerable<GenreDisplayViewModel> ReadAll()
        {
            List<Genre> genres;

            using (CinemaContext db = new CinemaContext())
            {
                genres = db.Genres.ToList();
            }

            return genres.Select(x => new GenreDisplayViewModel(x.Id, x.Title));
        }

        public void Update(Guid genreId, GenreCreateViewModel genreNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(genreId, genreNewData.Title))
            {
                throw new ArgumentNullException("Genre title or id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Genre genre = db.Genres.FirstOrDefault(x => x.Id == genreId);

                if (genreId == default)
                {
                    throw new EntityNotFoundException($"Genre with id \"{genreId}\" not found");
                }

                if (db.Genres.FirstOrDefault(x => x.Title == genreNewData.Title) != default)
                {
                    throw new DuplicatedEntityException($"Genre with title \"{genreNewData.Title}\" is already exist");
                }

                genre.Title = genreNewData.Title;

                db.SaveChanges();
            }
        }

        public bool IsEntityExist(string genreTitle)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(genreTitle))
            {
                return false;
            }

            using (CinemaContext db = new CinemaContext())
            {
                Genre genre = db.Genres.FirstOrDefault(x => x.Title == genreTitle);

                return genre != default;
            }
        }
    }
}

namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GenreService : IService<GenreNecessaryData, GenreDisplayedData>
    {
        public void Create(GenreNecessaryData genreData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(genreData.Title))
            {
                throw new ArgumentNullException("Title has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
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

        public IEnumerable<GenreDisplayedData> ReadAll()
        {
            List<Genre> genres;

            using (CinemaEntities db = new CinemaEntities())
            {
                genres = db.Genres.ToList();
            }

            return genres.Select(x => new GenreDisplayedData(x.Id, x.Title));
        }

        public void Update(Guid genreId, GenreNecessaryData genreNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(genreId, genreNewData.Title))
            {
                throw new ArgumentNullException("Genre title or id has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
            {
                Genre genre = db.Genres.FirstOrDefault(x => x.Title == genreTitle);

                return genre != default;
            }
        }
    }
}

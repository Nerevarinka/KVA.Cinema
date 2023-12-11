namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Director;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class DirectorService : IService<DirectorCreateViewModel, DirectorDisplayViewModel>
    {
        public void Create(DirectorCreateViewModel directorData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                if (db.Directors.FirstOrDefault(x => x.Name == directorData.Name) != default)
                {
                    throw new DuplicatedEntityException($"Director with name \"{directorData.Name}\" is already exist");
                }
            }

            Director newDirector = new Director()
            {
                Id = Guid.NewGuid(),
                Name = directorData.Name
            };

            using (CinemaContext db = new CinemaContext())
            {
                db.Directors.Add(newDirector);
                db.SaveChanges();
            }
        }

        public void Delete(Guid directorId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorId))
            {
                throw new ArgumentNullException("Director Id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Director director = db.Directors.FirstOrDefault(x => x.Id == directorId);

                if (director == default)
                {
                    throw new EntityNotFoundException($"Director with Id \"{directorId}\" not found");
                }

                db.Directors.Remove(director);
                db.SaveChanges();
            }
        }

        public IEnumerable<DirectorDisplayViewModel> ReadAll()
        {
            List<Director> directors;

            using (CinemaContext db = new CinemaContext())
            {
                directors = db.Directors.ToList();
            }

            return directors.Select(x => new DirectorDisplayViewModel(x.Id, x.Name));
        }

        public void Update(Guid directorId, DirectorCreateViewModel directorNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorId, directorNewData.Name))
            {
                throw new ArgumentNullException("Director name or id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Director director = db.Directors.FirstOrDefault(x => x.Id == directorId);

                if (directorId == default)
                {
                    throw new EntityNotFoundException($"Director with id \"{directorId}\" not found");
                }

                if (db.Directors.FirstOrDefault(x => x.Name == directorNewData.Name) != default)
                {
                    throw new DuplicatedEntityException($"Director with name \"{directorNewData.Name}\" is already exist");
                }

                director.Name = directorNewData.Name;

                db.SaveChanges();
            }
        }

        public bool IsEntityExist(string directorName)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorName))
            {
                return false;
            }

            using (CinemaContext db = new CinemaContext())
            {
                Director director = db.Directors.FirstOrDefault(x => x.Name == directorName);

                return director != default;
            }
        }
    }
}

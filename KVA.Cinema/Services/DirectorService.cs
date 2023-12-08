namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class DirectorService : IService<DirectorNecessaryData, DirectorDisplayedData>
    {
        public void Create(DirectorNecessaryData directorData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
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

        public IEnumerable<DirectorDisplayedData> ReadAll()
        {
            List<Director> directors;

            using (CinemaEntities db = new CinemaEntities())
            {
                directors = db.Directors.ToList();
            }

            return directors.Select(x => new DirectorDisplayedData(x.Id, x.Name));
        }

        public void Update(Guid directorId, DirectorNecessaryData directorNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorId, directorNewData.Name))
            {
                throw new ArgumentNullException("Director name or id has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
            {
                Director director = db.Directors.FirstOrDefault(x => x.Name == directorName);

                return director != default;
            }
        }
    }
}

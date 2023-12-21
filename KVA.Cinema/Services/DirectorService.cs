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
        public CinemaContext Context { get; set; }

        public void CreateAsync(DirectorCreateViewModel directorData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            if (Context.Directors.FirstOrDefault(x => x.Name == directorData.Name) != default)
            {
                throw new DuplicatedEntityException($"Director with name \"{directorData.Name}\" is already exist");
            }

            Director newDirector = new Director()
            {
                Id = Guid.NewGuid(),
                Name = directorData.Name
            };

            Context.Directors.Add(newDirector);
            Context.SaveChanges();
        }

        public void Delete(Guid directorId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorId))
            {
                throw new ArgumentNullException("Director Id has no value");
            }

            Director director = Context.Directors.FirstOrDefault(x => x.Id == directorId);

            if (director == default)
            {
                throw new EntityNotFoundException($"Director with Id \"{directorId}\" not found");
            }

            Context.Directors.Remove(director);
            Context.SaveChanges();
        }

        public IEnumerable<DirectorDisplayViewModel> ReadAll()
        {
            List<Director> directors;

            directors = Context.Directors.ToList();

            return directors.Select(x => new DirectorDisplayViewModel(x.Id, x.Name));
        }

        public void Update(Guid directorId, DirectorCreateViewModel directorNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorId, directorNewData.Name))
            {
                throw new ArgumentNullException("Director name or id has no value");
            }

            Director director = Context.Directors.FirstOrDefault(x => x.Id == directorId);

            if (directorId == default)
            {
                throw new EntityNotFoundException($"Director with id \"{directorId}\" not found");
            }

            if (Context.Directors.FirstOrDefault(x => x.Name == directorNewData.Name) != default)
            {
                throw new DuplicatedEntityException($"Director with name \"{directorNewData.Name}\" is already exist");
            }

            director.Name = directorNewData.Name;

            Context.SaveChanges();
        }

        public bool IsEntityExist(string directorName)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(directorName))
            {
                return false;
            }

            Director director = Context.Directors.FirstOrDefault(x => x.Name == directorName);

            return director != default;
        }

        public DirectorService(CinemaContext db)
        {
            Context = db;
        }
    }
}

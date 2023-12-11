namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Country;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class CountryService : IService<CountryCreateViewModel, CountryDisplayViewModel>
    {
        public void Create(CountryCreateViewModel countryData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                if (db.Countries.FirstOrDefault(x => x.Name == countryData.Name) != default)
                {
                    throw new DuplicatedEntityException($"Country with name \"{countryData.Name}\" is already exist");
                }
            }

            Country newCountry = new Country()
            {
                Id = Guid.NewGuid(),
                Name = countryData.Name
            };

            using (CinemaContext db = new CinemaContext())
            {
                db.Countries.Add(newCountry);
                db.SaveChanges();
            }
        }

        public void Delete(Guid countryId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryId))
            {
                throw new ArgumentNullException("Country Id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Country country = db.Countries.FirstOrDefault(x => x.Id == countryId);

                if (country == default)
                {
                    throw new EntityNotFoundException($"Country with Id \"{countryId}\" not found");
                }

                db.Countries.Remove(country);
                db.SaveChanges();
            }
        }

        public IEnumerable<CountryDisplayViewModel> ReadAll()
        {
            List<Country> countries;

            using (CinemaContext db = new CinemaContext())
            {
                countries = db.Countries.ToList();
            }

            return countries.Select(x => new CountryDisplayViewModel(x.Id, x.Name));
        }

        public void Update(Guid countryId, CountryCreateViewModel countryNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryId, countryNewData.Name))
            {
                throw new ArgumentNullException("Country name or id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Country country = db.Countries.FirstOrDefault(x => x.Id == countryId);

                if (countryId == default)
                {
                    throw new EntityNotFoundException($"Country with id \"{countryId}\" not found");
                }

                if (db.Countries.FirstOrDefault(x => x.Name == countryNewData.Name) != default)
                {
                    throw new DuplicatedEntityException($"Country with name \"{countryNewData.Name}\" is already exist");
                }

                country.Name = countryNewData.Name;

                db.SaveChanges();
            }
        }

        public bool IsEntityExist(string countryName)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryName))
            {
                return false;
            }

            using (CinemaContext db = new CinemaContext())
            {
                Country country = db.Countries.FirstOrDefault(x => x.Name == countryName);

                return country != default;
            }
        }
    }
}

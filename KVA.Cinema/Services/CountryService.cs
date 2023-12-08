namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Country;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class CountryService : IService<CountryCreateViewModel, CountryViewModel>
    {
        public void Create(CountryCreateViewModel countryData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
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

        public IEnumerable<CountryViewModel> ReadAll()
        {
            List<Country> countries;

            using (CinemaEntities db = new CinemaEntities())
            {
                countries = db.Countries.ToList();
            }

            return countries.Select(x => new CountryDisplayedData(x.Id, x.Name));
        }

        public void Update(Guid countryId, CountryCreateViewModel countryNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryId, countryNewData.Name))
            {
                throw new ArgumentNullException("Country name or id has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
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

            using (CinemaEntities db = new CinemaEntities())
            {
                Country country = db.Countries.FirstOrDefault(x => x.Name == countryName);

                return country != default;
            }
        }
    }
}

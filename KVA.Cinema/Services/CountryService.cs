﻿namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Country;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.ViewModels.Country;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CountryService : IService<CountryCreateViewModel, CountryDisplayViewModel, CountryEditViewModel>
    {
        /// <summary>
        /// Minimum length allowed for Name
        /// </summary>
        private const int NAME_LENGHT_MIN = 2;

        /// <summary>
        /// Maximum length allowed for Name
        /// </summary>
        private const int NAME_LENGHT_MAX = 128;

        private CinemaContext Context { get; set; }

        public CountryService(CinemaContext db)
        {
            Context = db;
        }

        public IEnumerable<CountryCreateViewModel> Read()
        {
            List<Country> countries = Context.Countries.ToList(); //TODO: перенести ToList в return

            return countries.Select(x => new CountryCreateViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public IEnumerable<CountryDisplayViewModel> ReadAll()
        {
            List<Country> countries = Context.Countries.ToList(); //TODO: перенести ToList в return

            return countries.Select(x => new CountryDisplayViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public void CreateAsync(CountryCreateViewModel countryData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            if (countryData.Name.Length < NAME_LENGHT_MIN)
            {
                throw new ArgumentException($"Length cannot be less than {NAME_LENGHT_MIN} symbols");
            }

            if (countryData.Name.Length > NAME_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {NAME_LENGHT_MAX} symbols");
            }

            if (Context.Countries.FirstOrDefault(x => x.Name == countryData.Name) != default)
            {
                throw new DuplicatedEntityException($"Country with name \"{countryData.Name}\" is already exist");
            }

            Country newCountry = new Country()
            {
                Id = Guid.NewGuid(),
                Name = countryData.Name
            };

            Context.Countries.Add(newCountry);
            Context.SaveChanges();
        }

        public void Delete(Guid countryId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryId))
            {
                throw new ArgumentNullException("Country Id has no value");
            }

            Country country = Context.Countries.FirstOrDefault(x => x.Id == countryId);

            if (country == default)
            {
                throw new EntityNotFoundException($"Country with Id \"{countryId}\" not found");
            }

            Context.Countries.Remove(country);
            Context.SaveChanges();
        }

        public void Update(Guid countryId, CountryEditViewModel countryNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryId, countryNewData.Name))
            {
                throw new ArgumentNullException("Country name or id has no value");
            }

            Country country = Context.Countries.FirstOrDefault(x => x.Id == countryId);

            if (country == default)
            {
                throw new EntityNotFoundException($"Country with id \"{countryId}\" not found");
            }

            if (countryNewData.Name.Length < NAME_LENGHT_MIN)
            {
                throw new ArgumentException($"Length cannot be less than {NAME_LENGHT_MIN} symbols");
            }

            if (countryNewData.Name.Length > NAME_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {NAME_LENGHT_MAX} symbols");
            }

            if (Context.Countries.FirstOrDefault(x => x.Name == countryNewData.Name && x.Id != countryNewData.Id) != default)
            {
                throw new DuplicatedEntityException($"Country with name \"{countryNewData.Name}\" is already exist");
            }

            country.Name = countryNewData.Name;

            Context.SaveChanges();
        }

        public bool IsEntityExist(Guid countryId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(countryId))
            {
                return false;
            }

            Country country = Context.Countries.FirstOrDefault(x => x.Id == countryId);

            return country != default;
        }
    }
}

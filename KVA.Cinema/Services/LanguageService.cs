﻿namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.ViewModels.Language;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguageService : IService<LanguageCreateViewModel, LanguageDisplayViewModel, LanguageEditViewModel>
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

        public LanguageService(CinemaContext db)
        {
            Context = db;
        }

        public IEnumerable<LanguageCreateViewModel> Read()
        {
            List<Language> languages = Context.Languages.ToList(); //TODO: перенести ToList в return

            return languages.Select(x => new LanguageCreateViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public IEnumerable<LanguageDisplayViewModel> ReadAll()
        {
            List<Language> languages = Context.Languages.ToList(); //TODO: перенести ToList в return

            return languages.Select(x => new LanguageDisplayViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
        }

        public void CreateAsync(LanguageCreateViewModel languageData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(languageData.Name))
            {
                throw new ArgumentNullException("Name has no value");
            }

            if (languageData.Name.Length < NAME_LENGHT_MIN)
            {
                throw new ArgumentException($"Length cannot be less than {NAME_LENGHT_MIN} symbols");
            }

            if (languageData.Name.Length > NAME_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {NAME_LENGHT_MAX} symbols");
            }

            if (Context.Languages.FirstOrDefault(x => x.Name == languageData.Name) != default)
            {
                throw new DuplicatedEntityException($"Language \"{languageData.Name}\" is already exist");
            }

            Language newLanguage = new Language()
            {
                Id = Guid.NewGuid(),
                Name = languageData.Name
            };

            Context.Languages.Add(newLanguage);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(id))
            {
                throw new ArgumentNullException("Language Id has no value");
            }

            Language language = Context.Languages.FirstOrDefault(x => x.Id == id);

            if (language == default)
            {
                throw new EntityNotFoundException($"Language with Id \"{id}\" not found");
            }

            Context.Languages.Remove(language);
            Context.SaveChanges();
        }

        public void Update(Guid id, LanguageEditViewModel languageNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(id, languageNewData.Name))
            {
                throw new ArgumentNullException("Language name or id has no value");
            }

            Language language = Context.Languages.FirstOrDefault(x => x.Id == id);

            if (id == default)
            {
                throw new EntityNotFoundException($"Language with id \"{id}\" not found");
            }

            if (languageNewData.Name.Length < NAME_LENGHT_MIN)
            {
                throw new ArgumentException($"Length cannot be less than {NAME_LENGHT_MIN} symbols");
            }

            if (languageNewData.Name.Length > NAME_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {NAME_LENGHT_MAX} symbols");
            }

            if (Context.Languages.FirstOrDefault(x => x.Name == languageNewData.Name && x.Id != languageNewData.Id) != default)
            {
                throw new DuplicatedEntityException($"Language \"{languageNewData.Name}\" is already exist");
            }

            language.Name = languageNewData.Name;

            Context.SaveChanges();
        }

        public bool IsEntityExist(Guid id)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(id))
            {
                return false;
            }

            Language language = Context.Languages.FirstOrDefault(x => x.Id == id);

            return language != default;
        }
    }
}

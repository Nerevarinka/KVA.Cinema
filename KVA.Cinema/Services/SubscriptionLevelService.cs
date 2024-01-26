﻿namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.ViewModels.SubscriptionLevel;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SubscriptionLevelService : IService<SubscriptionLevelCreateViewModel, SubscriptionLevelDisplayViewModel, SubscriptionLevelEditViewModel>
    {
        /// <summary>
        /// Minimum length allowed for Title
        /// </summary>
        private const int TITLE_LENGHT_MIN = 2;

        /// <summary>
        /// Maximum length allowed for Title
        /// </summary>
        private const int TITLE_LENGHT_MAX = 50;

        public CinemaContext Context { get; set; }

        public SubscriptionLevelService(CinemaContext db)
        {
            Context = db;
        }

        public IEnumerable<SubscriptionLevelCreateViewModel> Read()
        {
            List<SubscriptionLevel> levels = Context.SubscriptionLevels.ToList(); //TODO: перенести ToList в return

            return levels.Select(x => new SubscriptionLevelCreateViewModel()
            {
                Id = x.Id,
                Title = x.Title
            });
        }

        public IEnumerable<SubscriptionLevelDisplayViewModel> ReadAll()
        {
            List<SubscriptionLevel> subscriptionLevels = Context.SubscriptionLevels.ToList();

            return subscriptionLevels.Select(x => new SubscriptionLevelDisplayViewModel()
            {
                Id = x.Id,
                Title = x.Title
            });
        }

        public void CreateAsync(SubscriptionLevelCreateViewModel subscriptionLevelData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelData.Title))
            {
                throw new ArgumentNullException("Title has no value");
            }

            if (subscriptionLevelData.Title.Length < TITLE_LENGHT_MIN)
            {
                throw new ArgumentException($"Length cannot be less than {TITLE_LENGHT_MIN} symbols");
            }

            if (subscriptionLevelData.Title.Length > TITLE_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {TITLE_LENGHT_MAX} symbols");
            }

            if (Context.Countries.FirstOrDefault(x => x.Name == subscriptionLevelData.Title) != default)
            {
                throw new DuplicatedEntityException($"Subscription level with name \"{subscriptionLevelData.Title}\" is already exist");
            }

            if (Context.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionLevelData.Title) != default)
            {
                throw new DuplicatedEntityException($"Subscription level with title \"{subscriptionLevelData.Title}\" is already exist");
            }

            SubscriptionLevel newSubscriptionLevel = new SubscriptionLevel()
            {
                Id = Guid.NewGuid(),
                Title = subscriptionLevelData.Title
            };

            Context.SubscriptionLevels.Add(newSubscriptionLevel);
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(id))
            {
                throw new ArgumentNullException("Id has no value");
            }

            SubscriptionLevel subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Id == id);

            if (subscriptionLevel == default)
            {
                throw new EntityNotFoundException($"Subscription level with Id \"{id}\" not found");
            }

            Context.SubscriptionLevels.Remove(subscriptionLevel);
            Context.SaveChanges();
        }

        public void Update(Guid id, SubscriptionLevelEditViewModel subscriptionLevelNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(id, subscriptionLevelNewData.Title))
            {
                throw new ArgumentNullException("One or more parameters have no value");
            }

            if (subscriptionLevelNewData.Title.Length < TITLE_LENGHT_MIN)
            {
                throw new ArgumentException($"Length cannot be less than {TITLE_LENGHT_MIN} symbols");
            }

            if (subscriptionLevelNewData.Title.Length > TITLE_LENGHT_MAX)
            {
                throw new ArgumentException($"Length cannot be more than {TITLE_LENGHT_MAX} symbols");
            }

            SubscriptionLevel subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Id == id);

            if (id == default)
            {
                throw new EntityNotFoundException($"Subscription level with id \"{id}\" not found");
            }

            if (Context.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionLevelNewData.Title && x.Id != subscriptionLevelNewData.Id) != default)
            {
                throw new DuplicatedEntityException($"Subscription level with title \"{subscriptionLevelNewData.Title}\" is already exist");
            }

            subscriptionLevel.Title = subscriptionLevelNewData.Title;

            Context.SaveChanges();
        }

        public bool IsEntityExist(Guid id)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(id))
            {
                return false;
            }

            SubscriptionLevel subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Id == id);

            return subscriptionLevel != default;
        }
    }
}

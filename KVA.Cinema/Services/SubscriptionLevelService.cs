namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.SubscriptionLevel;
    using KVA.Cinema.Models.ViewModels.SubscriptionLevel;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class SubscriptionLevelService : IService<SubscriptionLevelCreateViewModel, SubscriptionLevelDisplayViewModel, SubscriptionLevelEditViewModel>
    {
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

        public void Delete(Guid subscriptionLevelId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            SubscriptionLevel subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Id == subscriptionLevelId);

            if (subscriptionLevel == default)
            {
                throw new EntityNotFoundException($"Level with Id \"{subscriptionLevelId}\" not found");
            }

            Context.SubscriptionLevels.Remove(subscriptionLevel);
            Context.SaveChanges();
        }

        public void Update(Guid subscriptionLevelId, SubscriptionLevelEditViewModel subscriptionLevelNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelId, subscriptionLevelNewData.Title))
            {
                throw new ArgumentNullException("One or more parameters have no value");
            }

            SubscriptionLevel subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Id == subscriptionLevelId);

            if (subscriptionLevelId == default)
            {
                throw new EntityNotFoundException($"Subscription level with id \"{subscriptionLevelId}\" not found");
            }

            if (Context.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionLevelNewData.Title) != default)
            {
                throw new DuplicatedEntityException($"Subscription level with title \"{subscriptionLevelNewData.Title}\" is already exist");
            }

            subscriptionLevel.Title = subscriptionLevelNewData.Title;

            Context.SaveChanges();
        }

        public bool IsEntityExist(Guid subscriptionLevelId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelId))
            {
                return false;
            }

            SubscriptionLevel subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Id == subscriptionLevelId);

            return subscriptionLevel != default;
        }
    }
}

namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models.SubscriptionLevel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class SubscriptionLevelService : IService<SubscriptionLevelCreateViewModel, SubscriptionLevelViewModel>
    {
        public void Create(SubscriptionLevelCreateViewModel subscriptionLevelData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelData.Title))
            {
                throw new ArgumentNullException("Title has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
            {
                if (db.Levels.FirstOrDefault(x => x.Title == subscriptionLevelData.Title) != default)
                {
                    throw new DuplicatedEntityException($"Subscription level with title \"{subscriptionLevelData.Title}\" is already exist");
                }
            }

            Level newSubscriptionLevel = new Level()
            {
                Id = Guid.NewGuid(),
                Title = subscriptionLevelData.Title
            };

            using (CinemaEntities db = new CinemaEntities())
            {
                db.Levels.Add(newSubscriptionLevel);
                db.SaveChanges();
            }
        }

        public void Delete(Guid subscriptionLevelId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
            {
                Level subscriptionLevel = db.Levels.FirstOrDefault(x => x.Id == subscriptionLevelId);

                if (subscriptionLevel == default)
                {
                    throw new EntityNotFoundException($"Level with Id \"{subscriptionLevelId}\" not found");
                }

                db.Levels.Remove(subscriptionLevel);
                db.SaveChanges();
            }
        }

        public IEnumerable<SubscriptionLevelViewModel> ReadAll()
        {
            List<Level> subscriptionLevels;

            using (CinemaEntities db = new CinemaEntities())
            {
                subscriptionLevels = db.Levels.ToList();
            }

            return subscriptionLevels.Select(x => new SubscriptionLevelDisplayedData(x.Id, x.Title));
        }

        public void Update(Guid subscriptionLevelId, SubscriptionLevelCreateViewModel subscriptionLevelNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelId, subscriptionLevelNewData.Title))
            {
                throw new ArgumentNullException("One or more parameters have no value");
            }

            Level subscriptionLevel;

            using (CinemaEntities db = new CinemaEntities())
            {
                subscriptionLevel = db.Levels.FirstOrDefault(x => x.Id == subscriptionLevelId);

                if (subscriptionLevelId == default)
                {
                    throw new EntityNotFoundException($"Subscription level with id \"{subscriptionLevelId}\" not found");
                }

                if (db.Levels.FirstOrDefault(x => x.Title == subscriptionLevelNewData.Title) != default)
                {
                    throw new DuplicatedEntityException($"Subscription level with title \"{subscriptionLevelNewData.Title}\" is already exist");
                }
            }

            subscriptionLevel.Title = subscriptionLevelNewData.Title;

            using (CinemaEntities db = new CinemaEntities())
                db.SaveChanges();
        }

        public bool IsEntityExist(string subscriptionLevelTitle)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionLevelTitle))
            {
                return false;
            }

            using (CinemaEntities db = new CinemaEntities())
            {
                Level subscriptionLevel = db.Levels.FirstOrDefault(x => x.Title == subscriptionLevelTitle);

                return subscriptionLevel != default;
            }
        }
    }
}

namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.Subscription;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SubscriptionService : IService<SubscriptionCreateViewModel, SubscriptionDisplayViewModel>
    {
        private const int MIN_DURATION_DAYS = 1;
        private const int MAX_DURATION_DAYS = 365;

        public void Create(SubscriptionCreateViewModel subscriptionData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionData.Title,
                                                        subscriptionData.Description,
                                                        subscriptionData.Cost,
                                                        subscriptionData.Level,
                                                        subscriptionData.ReleasedIn,
                                                        subscriptionData.Duration,
                                                        subscriptionData.AvailableUntil))
            {
                throw new ArgumentNullException("One or more parameters have no value");
            }

            if (subscriptionData.Duration < MIN_DURATION_DAYS || subscriptionData.Duration > MAX_DURATION_DAYS)
            {
                throw new ArgumentException($"Duration can be in range {MIN_DURATION_DAYS}-{MAX_DURATION_DAYS} days");
            }

            SubscriptionLevel subscriptionLevel;

            using (CinemaContext db = new CinemaContext())
            {
                if (db.Subscriptions.FirstOrDefault(x => x.Title == subscriptionData.Title) != default)
                {
                    throw new DuplicatedEntityException($"Subscription with title \"{subscriptionData.Title}\" is already exist");
                }

                subscriptionLevel = db.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionData.Level);
            }

            if (subscriptionLevel == default)
            {
                throw new EntityNotFoundException($"Subscription level \"{subscriptionData.Level}\" not found");
            }

            Subscription newSubscription = new Subscription()
            {
                Id = Guid.NewGuid(),
                Title = subscriptionData.Title,
                Description = subscriptionData.Description,
                Cost = subscriptionData.Cost,
                LevelId = subscriptionLevel.Id,
                ReleasedIn = subscriptionData.ReleasedIn,
                Duration = subscriptionData.Duration,
                AvailableUntil = subscriptionData.AvailableUntil
            };

            using (CinemaContext db = new CinemaContext())
            {
                db.Subscriptions.Add(newSubscription);
                db.SaveChanges();
            }
        }

        public void Delete(Guid subscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            using (CinemaContext db = new CinemaContext())
            {
                Subscription subscription = db.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

                if (subscription == default)
                {
                    throw new EntityNotFoundException($"Subscription with Id \"{subscriptionId}\" not found");
                }

                db.Subscriptions.Remove(subscription);
                db.SaveChanges();
            }
        }

        public IEnumerable<SubscriptionDisplayViewModel> ReadAll()
        {
            IEnumerable<SubscriptionDisplayViewModel> subscriptions;

            using (CinemaContext db = new CinemaContext())
            {
                subscriptions = db.Subscriptions   // подумать, как переделать чудовище
                    .ToList()
                    .Select(x =>
                        new SubscriptionDisplayViewModel(
                            x.Id,
                            x.Title,
                            x.Description,
                            x.Cost,
                            x.Level.Title,
                            x.ReleasedIn,
                            x.Duration,
                            x.AvailableUntil
                        )
                    )
                    .ToList()
                    ;

            }
            return subscriptions;
        }

        public void Update(Guid subscriptionId, SubscriptionCreateViewModel subscriptionNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionId,
                                                        subscriptionNewData.Title,
                                                        subscriptionNewData.Description,
                                                        subscriptionNewData.Cost,
                                                        subscriptionNewData.Level,
                                                        subscriptionNewData.ReleasedIn,
                                                        subscriptionNewData.Duration,
                                                        subscriptionNewData.AvailableUntil))
            {

                throw new ArgumentNullException("One or more parameters have no value");
            }

            if (subscriptionNewData.Duration < MIN_DURATION_DAYS || subscriptionNewData.Duration > MAX_DURATION_DAYS)
            {
                throw new ArgumentException($"Duration can be in range {MIN_DURATION_DAYS}-{MAX_DURATION_DAYS} days");
            }

            Subscription subscription;
            SubscriptionLevel subscriptionLevel;

            using (CinemaContext db = new CinemaContext())
            {
                subscription = db.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

                if (subscription == default)
                {
                    throw new EntityNotFoundException($"Subscription with id \"{subscriptionId}\" not found");
                }

                if (db.Subscriptions.FirstOrDefault(x => x.Title == subscriptionNewData.Title) != default)
                {
                    throw new DuplicatedEntityException($"Subscription with title \"{subscriptionNewData.Title}\" is already exist");
                }

                subscriptionLevel = db.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionNewData.Level);
            }

            if (subscriptionLevel == default)
            {
                throw new EntityNotFoundException($"Subscription level \"{subscriptionNewData.Level}\" not found");
            }

            subscription.Title = subscriptionNewData.Title;
            subscription.Description = subscriptionNewData.Description;
            subscription.Cost = subscriptionNewData.Cost;
            subscription.LevelId = subscriptionLevel.Id;
            subscription.ReleasedIn = subscriptionNewData.ReleasedIn;
            subscription.Duration = subscriptionNewData.Duration;
            subscription.AvailableUntil = subscriptionNewData.AvailableUntil;

            using (CinemaContext db = new CinemaContext())
                db.SaveChanges();
        }

        public bool IsEntityExist(string subscriptionTitle)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionTitle))
            {
                return false;
            }

            using (CinemaContext db = new CinemaContext())
            {
                Subscription subscription = db.Subscriptions.FirstOrDefault(x => x.Title == subscriptionTitle);

                return subscription != default;
            }
        }
    }
}

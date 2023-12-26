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
        private const int DURATION_DAYS_MIN = 1;
        private const int DURATION_DAYS_MAX = 365;

        public CinemaContext Context { get; set; }

        public SubscriptionService(CinemaContext db)
        {
            Context = db;
        }

        public void CreateAsync(SubscriptionCreateViewModel subscriptionData)
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

            if (subscriptionData.Duration < DURATION_DAYS_MIN || subscriptionData.Duration > DURATION_DAYS_MAX)
            {
                throw new ArgumentException($"Duration can be in range {DURATION_DAYS_MIN}-{DURATION_DAYS_MAX} days");
            }

            SubscriptionLevel subscriptionLevel;

            if (Context.Subscriptions.FirstOrDefault(x => x.Title == subscriptionData.Title) != default)
            {
                throw new DuplicatedEntityException($"Subscription with title \"{subscriptionData.Title}\" is already exist");
            }

            subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionData.Level);

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

            Context.Subscriptions.Add(newSubscription);
            Context.SaveChanges();
        }

        public void Delete(Guid subscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            Subscription subscription = Context.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

            if (subscription == default)
            {
                throw new EntityNotFoundException($"Subscription with Id \"{subscriptionId}\" not found");
            }

            Context.Subscriptions.Remove(subscription);
            Context.SaveChanges();
        }

        public IEnumerable<SubscriptionDisplayViewModel> ReadAll()
        {
            IEnumerable<SubscriptionDisplayViewModel> subscriptions;

            subscriptions = Context.Subscriptions   // подумать, как переделать чудовище - AutoMapper
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

            if (subscriptionNewData.Duration < DURATION_DAYS_MIN || subscriptionNewData.Duration > DURATION_DAYS_MAX)
            {
                throw new ArgumentException($"Duration can be in range {DURATION_DAYS_MIN}-{DURATION_DAYS_MAX} days");
            }

            Subscription subscription;
            SubscriptionLevel subscriptionLevel;

            subscription = Context.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

            if (subscription == default)
            {
                throw new EntityNotFoundException($"Subscription with id \"{subscriptionId}\" not found");
            }

            if (Context.Subscriptions.FirstOrDefault(x => x.Title == subscriptionNewData.Title) != default)
            {
                throw new DuplicatedEntityException($"Subscription with title \"{subscriptionNewData.Title}\" is already exist");
            }

            subscriptionLevel = Context.SubscriptionLevels.FirstOrDefault(x => x.Title == subscriptionNewData.Level);

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

            Context.SaveChanges();
        }

        public bool IsEntityExist(Guid subscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionId))
            {
                return false;
            }

            Subscription subscription = Context.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

            return subscription != default;
        }

        public IEnumerable<SubscriptionCreateViewModel> Read()
        {
            throw new NotImplementedException();
        }
    }
}

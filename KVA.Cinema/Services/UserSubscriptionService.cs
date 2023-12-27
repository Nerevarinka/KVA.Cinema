namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.UserSubscription;
    using KVA.Cinema.Models.ViewModels.UserSubscription;
    using KVA.Cinema.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class UserSubscriptionService : IService<UserSubscriptionCreateViewModel, UserSubscriptionDisplayViewModel, UserSubscriptionEditViewModel>
    {
        public CinemaContext Context { get; set; }

        public UserSubscriptionService(CinemaContext db)
        {
            Context = db;
        }

        public IEnumerable<UserSubscriptionCreateViewModel> Read()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserSubscriptionDisplayViewModel> ReadAll()
        {
            List<UserSubscription> userSubscriptions = Context.UserSubscriptions.ToList();

            return (IEnumerable<UserSubscriptionDisplayViewModel>)userSubscriptions.Select(x => new UserSubscriptionCreateViewModel()
            {
                Id = x.Id,
                SubscriptionId = x.SubscriptionId,
                UserId = x.UserId,
                ActivatedOn = x.ActivatedOn,
                LastUntil = x.LastUntil
                });
        }

        public void CreateAsync(UserSubscriptionCreateViewModel userSubscriptionData) // после окончания подписки должна быть возможность добавить такую же опять
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userSubscriptionData.Id,
                                                        userSubscriptionData.SubscriptionId,
                                                        userSubscriptionData.UserId,
                                                        userSubscriptionData.ActivatedOn,
                                                        userSubscriptionData.LastUntil))
            {
                throw new ArgumentNullException("One or more parameters have no value"); // добавить catch в меню
            }

            UserSubscription newUserSubscription = new UserSubscription()
            {
                Id = Guid.NewGuid(),
                SubscriptionId = userSubscriptionData.SubscriptionId,
                UserId = userSubscriptionData.UserId,
                ActivatedOn = userSubscriptionData.ActivatedOn,
                LastUntil = userSubscriptionData.LastUntil
            };

            UserSubscription subscription = Context.UserSubscriptions.FirstOrDefault(x => x.SubscriptionId == userSubscriptionData.SubscriptionId
                                                                                && x.UserId == userSubscriptionData.UserId);

            if (subscription != default)
            {
                throw new DuplicatedEntityException($"Subscription with id \"{userSubscriptionData.SubscriptionId}\" is already exist for this user");
            }

            Context.UserSubscriptions.Add(newUserSubscription);
            Context.SaveChanges();
        }

        public void Delete(Guid userSubscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userSubscriptionId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            UserSubscription userSubscription = Context.UserSubscriptions.FirstOrDefault(x => x.Id == userSubscriptionId);

            if (userSubscription == default)
            {
                throw new EntityNotFoundException($"User's subscription with Id \"{userSubscriptionId}\" not found");
            }

            Context.UserSubscriptions.Remove(userSubscription);
            Context.SaveChanges();
        }

        public void Update(Guid userSubscriptionId, UserSubscriptionEditViewModel userSubscriptionNewData)
        {
            throw new NotImplementedException();

            //if (CheckUtilities.ContainsNullOrEmptyValue(userSubscriptionId,
            //                                            userSubscriptionNewData.Id,
            //                                            userSubscriptionNewData.SubscriptionId,
            //                                            userSubscriptionNewData.UserId,
            //                                            userSubscriptionNewData.ActivatedOn,
            //                                            userSubscriptionNewData.LastUntil))
            //{
            //    throw new ArgumentNullException("One or more parameters have no value");
            //}

            //using (CinemaEntities db = new CinemaEntities())
            //{
            //    UserSubscription userSubscription = db.UserSubscriptions.FirstOrDefault(x => x.Id == userSubscriptionId);

            //    if (userSubscription == default)
            //    {
            //        throw new EntityNotFoundException($"User's subscription with id \"{userSubscriptionId}\" not found");
            //    }

            //    if (db.UserSubscriptions.FirstOrDefault(x => x.SubscriptionId == userSubscription.SubscriptionId) != default)
            //    {
            //        throw new DuplicatedEntityException($"Subscription with id \"{userSubscription.SubscriptionId}\" is already exist for this user");
            //    }

            //    userSubscription.SubscriptionId = userSubscriptionNewData.SubscriptionId;
            //    userSubscription.UserId = userSubscriptionNewData.UserId;
            //    userSubscription.ActivatedOn = userSubscriptionNewData.ActivatedOn;
            //    userSubscription.UserId = userSubscriptionNewData.UserId;
            //    userSubscription.LastUntil = userSubscriptionNewData.LastUntil;

            //    db.SaveChanges();
            //}
        }

        ///*        public bool IsEntityExist(string subscriptionTitle)*/ // принимает член Subscription, а не UserSubscription
        //        //{
        //        //    throw new NotImplementedException();

        //            //if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionTitle))
        //            //{
        //            //    return false;
        //            //}

        //            //using (CinemaEntities db = new CinemaEntities())
        //            //{
        //            //    Subscription subscription = db.Subscriptions.FirstOrDefault(x => x.Title == subscriptionTitle);

        //            //    return subscription != default;
        //            //}
        //        }

        public bool IsEntityExist(Guid subscriptionId) // принимает член Subscription, а не UserSubscription
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(subscriptionId))
            {
                return false;
            }

            Subscription subscription = Context.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

            return subscription != default;
        }
    }
}

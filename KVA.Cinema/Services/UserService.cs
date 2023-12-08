namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : IService<UserNecessaryData, UserDisplayedData>
    {
        private const int MIN_AGE = 14;

        public IEnumerable<UserDisplayedData> ReadAll()
        {
            List<User> users;

            using (CinemaEntities db = new CinemaEntities())
            {
                users = db.Users.ToList();
            }

            return users.Select(x => new UserDisplayedData(x.Id,
                                                           x.FirstName,
                                                           x.LastName,
                                                           x.Nickname,
                                                           x.BirthDate,
                                                           x.Email));
        }

        public void Create(UserNecessaryData userData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userData.FirstName,
                                                        userData.LastName,
                                                        userData.Nickname,
                                                        userData.BirthDate,
                                                        userData.Email))
            {
                throw new ArgumentNullException("One or more user's parameters have no value");
            }

            if (userData.BirthDate > DateTime.Now.AddYears(-MIN_AGE))
            {
                throw new ArgumentException($"Age must not be less than {MIN_AGE}");
            }

            using (CinemaEntities db = new CinemaEntities())
            {
                if (db.Users.FirstOrDefault(x => x.Nickname == userData.Nickname) != default)
                {
                    throw new DuplicatedEntityException($"User with nickname \"{userData.Nickname}\" is already exist"); // сделать возможность ввести значение
                                                                                                                         // заново, не сбрасывая предыдущие
                }

                if (db.Users.FirstOrDefault(x => x.Email == userData.Email) != default)
                {
                    throw new DuplicatedEntityException($"User with email \"{userData.Email}\" is already exist");
                }
            }

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Nickname = userData.Nickname,
                BirthDate = userData.BirthDate,
                Email = userData.Email,
                IsActive = true
            };

            using (CinemaEntities db = new CinemaEntities())
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }

        public void Update(Guid userId, UserNecessaryData userNewData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId,
                                                        userNewData.LastName,
                                                        userNewData.FirstName,
                                                        userNewData.Nickname,
                                                        userNewData.BirthDate,
                                                        userNewData.Email))
            {
                throw new ArgumentNullException("One or more user's parameters have no value");
            }

            if (userNewData.BirthDate > DateTime.Now.AddYears(-MIN_AGE))
            {
                throw new ArgumentException($"Age must not be less than {MIN_AGE}");
            }

            User user;

            using (CinemaEntities db = new CinemaEntities())
            {
                user = db.Users.FirstOrDefault(x => x.Id == userId);

                if (user == default)
                {
                    throw new EntityNotFoundException($"User with id \"{userId}\" not found");
                }

                if (db.Users.FirstOrDefault(x => x.Nickname == userNewData.Nickname) != default)
                {
                    throw new DuplicatedEntityException($"User with nickname \"{userNewData.Nickname}\" is already exist");
                }

                if (db.Users.FirstOrDefault(x => x.Email == userNewData.Email) != default)
                {
                    throw new DuplicatedEntityException($"User with email \"{userNewData.Email}\" is already exist");
                }
            }

            user.FirstName = userNewData.FirstName;
            user.LastName = userNewData.LastName;
            user.Nickname = userNewData.Nickname;
            user.BirthDate = userNewData.BirthDate;
            user.Email = userNewData.Email;

            // ASS TO THINK: А если мы ввели те же самые данные пользователя - то имеет ли тут смысл вызывать сохранение бд?
            using (CinemaEntities db = new CinemaEntities())
                db.SaveChanges();
        }

        public void Delete(Guid userId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId))
            {
                throw new ArgumentNullException("User Id has no value");
            }

            using (CinemaEntities db = new CinemaEntities())
            {
                User user = db.Users.FirstOrDefault(x => x.Id == userId);

                if (user == default)
                {
                    throw new EntityNotFoundException($"User with Id \"{userId}\" not found");
                }

                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public void AddSubscription(Guid userId, Guid subscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId, subscriptionId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            Subscription subscription;

            using (CinemaEntities db = new CinemaEntities())
            {
                User user = db.Users.FirstOrDefault(x => x.Id == userId);

                if (user == default)
                {
                    throw new EntityNotFoundException($"User with Id \"{userId}\" not found");
                }

                subscription = db.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);
            }

            if (subscription == default)
            {
                throw new EntityNotFoundException($"Subscription with Id \"{subscriptionId}\" not found");
            }

            new UserSubscriptionController().Create(new UserSubscriptionNecessaryData(Guid.NewGuid(),
                                                                                      subscriptionId,
                                                                                      userId,
                                                                                      DateTime.Now,
                                                                                      DateTime.Now.AddYears(1)));
        }

        public void RemoveSubscription(Guid userId, Guid subscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId, subscriptionId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            UserSubscription userSubscription;

            using (CinemaEntities db = new CinemaEntities())
            {
                User user = db.Users.FirstOrDefault(x => x.Id == userId);

                if (user == default)
                {
                    throw new EntityNotFoundException($"User with Id \"{userId}\" not found");
                }

                userSubscription = db.UserSubscriptions.FirstOrDefault(x => x.SubscriptionId == subscriptionId);
            }

            new UserSubscriptionController().Delete(userSubscription.Id);
        }

        public bool IsEntityExist(string nickname)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(nickname))
            {
                return false;
            }

            using (CinemaEntities db = new CinemaEntities())
            {
                User user = db.Users.FirstOrDefault(x => x.Nickname == nickname);

                return user != default;
            }
        }
    }
}

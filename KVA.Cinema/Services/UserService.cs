namespace KVA.Cinema.Services
{
    using KVA.Cinema.Exceptions;
    using KVA.Cinema.Models;
    using KVA.Cinema.Models.Entities;
    using KVA.Cinema.Models.User;
    using KVA.Cinema.Models.UserSubscription;
    using KVA.Cinema.Utilities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class UserService : IService<UserCreateViewModel, UserDisplayViewModel>
    {
        /// <summary>
        /// Minimum age allowed to use app
        /// </summary>
        private const int AGE_MIN = 14;

        /// <summary>
        /// Maximum age allowed to use app
        /// </summary>
        private const int AGE_MAX = 120;

        /// <summary>
        /// Pattern to check email
        /// </summary>
        private const string EMAIL_PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        /// <summary>
        /// Minimum length allowed for Last Name, First Name and Nickname
        /// </summary>
        private const int NAME_LENGHT_MIN = 2;

        /// <summary>
        /// Maximum length allowed for Last Name, First Name and Nickname
        /// </summary>
        private const int NAME_LENGHT_MAX = 20;

        /// <summary>
        /// Minimum password length
        /// </summary>
        private const int PASSWORD_LENGHT_MIN = 8;

        /// <summary>
        /// Maximum password length
        /// </summary>
        private const int PASSWORD_LENGHT_MAX = 120;

        /// <summary>
        /// Last Name, First Name and Nickname
        /// </summary>
        private string[] names;

        /// <summary>
        /// User management service
        /// </summary>
        public UserManager<User> UserManager { get; }

        /// <summary>
        /// User authentication service
        /// </summary>
        public SignInManager<User> SignInManager { get; }

        private CinemaContext Context { get; }

        public UserService(CinemaContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            Context = db;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IEnumerable<UserCreateViewModel> Read()
        {
            List<User> users = Context.Users.ToList();

            return users.Select(x => new UserCreateViewModel()
            {
                Id=x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nickname = x.Nickname,
                BirthDate = x.BirthDate,
                Email = x.Email
            });
        }

        public IEnumerable<UserDisplayViewModel> ReadAll()
        {
            List<User> users = Context.Users.ToList();

            return users.Select(x => new UserDisplayViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Nickname = x.Nickname,
                BirthDate = x.BirthDate,
                Email = x.Email
            });
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="userData"></param>
        /// <exception cref="DuplicatedEntityException">Nickname is not unique</exception>
        /// <exception cref="FailedToCreateEntityException">Failed to create user</exception>
        /// <returns></returns>
        public async Task CreateAsync(UserCreateViewModel userData)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userData.FirstName,
                                                        userData.LastName,
                                                        userData.Nickname,
                                                        userData.BirthDate,
                                                        userData.Email,
                                                        userData.Password,
                                                        userData.PasswordConfirm))
            {
                throw new ArgumentNullException("One or more required fields have no value");
            }

            if (userData.BirthDate > DateTime.Now.AddYears(-AGE_MIN) || userData.BirthDate < DateTime.Now.AddYears(-AGE_MAX))
            {
                throw new ArgumentException($"Age must be in {AGE_MIN}-{AGE_MAX}");
            }

            if (!new Regex(EMAIL_PATTERN).IsMatch(userData.Email))
            {
                throw new ArgumentException("Incorrect email format");
            }

            names = new string[] { userData.FirstName, userData.LastName, userData.Nickname };

            foreach (var item in names)
            {
                if (item.Length is < NAME_LENGHT_MIN or > NAME_LENGHT_MAX)
                {
                    throw new ArgumentException($"Length must be in {NAME_LENGHT_MIN}-{NAME_LENGHT_MAX} symbols");
                }
            }

            if (userData.Password.Length is < PASSWORD_LENGHT_MIN or > PASSWORD_LENGHT_MAX)
            {
                throw new ArgumentException($"Password length must be in {PASSWORD_LENGHT_MIN}-{PASSWORD_LENGHT_MAX} symbols");
            }

            if (userData.Password != userData.PasswordConfirm)
            {
                throw new ArgumentException("Passwords are not equal");
            }

            if (Context.Users.FirstOrDefault(x => x.Nickname == userData.Nickname) != default)
            {
                throw new DuplicatedEntityException($"User with nickname \"{userData.Nickname}\" is already exist");
            }

            if (Context.Users.FirstOrDefault(x => x.Email == userData.Email) != default)
            {
                throw new DuplicatedEntityException($"User with email \"{userData.Email}\" is already exist");
            }

            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = userData.Nickname,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Nickname = userData.Nickname,
                BirthDate = userData.BirthDate,
                Email = userData.Email,
                RegisteredOn = DateTime.Now,
                LastVisit = DateTime.Now,
                IsActive = true
            };

            var result = await UserManager.CreateAsync(newUser, userData.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(newUser, false);
            }
            else
            {
                throw new FailedToCreateEntityException(result.Errors);
            }
        }

        void IService<UserCreateViewModel, UserDisplayViewModel>.CreateAsync(UserCreateViewModel entityData)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid userId, UserCreateViewModel userNewData)
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

            if (userNewData.BirthDate > DateTime.Now.AddYears(-AGE_MIN))
            {
                throw new ArgumentException($"Age must not be less than {AGE_MIN}");
            }

            User user = Context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == default)
            {
                throw new EntityNotFoundException($"User with id \"{userId}\" not found");
            }

            if (Context.Users.FirstOrDefault(x => x.Nickname == userNewData.Nickname) != default)
            {
                throw new DuplicatedEntityException($"User with nickname \"{userNewData.Nickname}\" is already exist");
            }

            if (Context.Users.FirstOrDefault(x => x.Email == userNewData.Email) != default)
            {
                throw new DuplicatedEntityException($"User with email \"{userNewData.Email}\" is already exist");
            }

            user.FirstName = userNewData.FirstName;
            user.LastName = userNewData.LastName;
            user.Nickname = userNewData.Nickname;
            user.BirthDate = userNewData.BirthDate;
            user.Email = userNewData.Email;

            // ASS TO THINK: А если мы ввели те же самые данные пользователя - то имеет ли тут смысл вызывать сохранение бд?
            Context.SaveChanges();
        }

        public void Delete(Guid userId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId))
            {
                throw new ArgumentNullException("User Id has no value");
            }

            User user = Context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == default)
            {
                throw new EntityNotFoundException($"User with Id \"{userId}\" not found");
            }

            Context.Users.Remove(user);
            Context.SaveChanges();
        }

        public void AddSubscription(Guid userId, Guid subscriptionId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId, subscriptionId))
            {
                throw new ArgumentNullException("Id has no value");
            }

            User user = Context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == default)
            {
                throw new EntityNotFoundException($"User with Id \"{userId}\" not found");
            }

            Subscription subscription = Context.Subscriptions.FirstOrDefault(x => x.Id == subscriptionId);

            if (subscription == default)
            {
                throw new EntityNotFoundException($"Subscription with Id \"{subscriptionId}\" not found");
            }

            new UserSubscriptionService(Context).CreateAsync(new UserSubscriptionCreateViewModel(Guid.NewGuid(),
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

            User user = Context.Users.FirstOrDefault(x => x.Id == userId);

            if (user == default)
            {
                throw new EntityNotFoundException($"User with Id \"{userId}\" not found");
            }

            UserSubscription userSubscription = Context.UserSubscriptions.FirstOrDefault(x => x.SubscriptionId == subscriptionId);

            new UserSubscriptionService(Context).Delete(userSubscription.Id);
        }

        public bool IsEntityExist(Guid userId)
        {
            if (CheckUtilities.ContainsNullOrEmptyValue(userId))
            {
                return false;
            }

            User user = Context.Users.FirstOrDefault(x => x.Id == userId);

            return user != default;
        }
    }
}

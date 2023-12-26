using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVA.Cinema.Models;
using KVA.Cinema.Models.Entities;
using KVA.Cinema.Models.User;
using KVA.Cinema.Services;
using KVA.Cinema.Exceptions;
using Microsoft.AspNetCore.Identity;
using KVA.Cinema.Models.ViewModels.User;

namespace KVA.Cinema.Controllers    //TODO: replace NotFound()
{
    public class UsersController : Controller
    {
        private UserService UserService { get; }

        public UsersController(UserService userService)
        {
            UserService = userService;
        }

        // GET: Users
        public IActionResult Index()
        {
            var data = UserService.ReadAll();
            return View(data);
        }

        // GET: Users/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = UserService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel userData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await UserService.CreateAsync(userData);
                    return RedirectToAction(nameof(Index));
                }
                catch (FailedToCreateEntityException ex)
                {
                    foreach (var error in ex.Errors)
                    {
                        ModelState.AddModelError(string.Empty, (error as IdentityError).Description);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(userData);
        }

        // GET: Users/Edit/5
        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = UserService.Read()
                .FirstOrDefault(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userCreateModel = new UserCreateViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Nickname=user.Nickname,
                BirthDate = user.BirthDate,
                Email = user.Email
            };

            //ViewBag.Id = user.Id;

            return View(userCreateModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, UserCreateViewModel userNewData)
        {
            if (id != ViewBag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UserService.Update(id, userNewData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(userNewData);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = UserService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var user = UserService.ReadAll()
                .FirstOrDefault(m => m.Id == id);
            UserService.Delete(user.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { /*ReturnUrl = returnUrl*/ });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await UserService.SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    //if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    //{
                    //    return Redirect(model.ReturnUrl);
                    //}
                    //else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or/and password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await UserService.SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool UserExists(Guid id)
        {
            return UserService.IsEntityExist(id);
        }
    }
}

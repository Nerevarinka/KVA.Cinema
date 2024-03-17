﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVA.Cinema.Models;
using KVA.Cinema.Models.Entities;
using KVA.Cinema.Services;
using KVA.Cinema.Models.ViewModels.Subscription;
using KVA.Cinema.Models.ViewModels.Video;

namespace KVA.Cinema.Controllers
{
    public class SubscriptionsController : Controller
    {
        private SubscriptionService SubscriptionService { get; set; }

        private SubscriptionLevelService SubscriptionLevelService { get; set; }

        private UserService UserService { get; set; }

        private VideoService VideoService { get; set; }

        public SubscriptionsController(SubscriptionService subscriptionService, SubscriptionLevelService subscriptionLevelService, UserService userService, VideoService videoService)
        {
            SubscriptionService = subscriptionService;
            SubscriptionLevelService = subscriptionLevelService;
            UserService = userService;
            VideoService = videoService;
        }

        // GET: Subscriptions
        public IActionResult Index()
        {
            var subscriptions = SubscriptionService.ReadAll()/*.Where(x => x.VideosInSubscription.Count() != 0)*/; //TODO: обычным юзерам показывать только подписки с видео, админам - все

            if (!User.Identity.IsAuthenticated)
            {
                return View(subscriptions);
            }

            var user = UserService.ReadAll().FirstOrDefault(m => m.Nickname == User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            foreach (var subscription in subscriptions)
            {
                subscription.IsPurchasedByCurrentUser = user.SubscriptionIds.Any(m => m == subscription.Id);
            }

            return View(subscriptions);
        }

        // GET: Subscriptions/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SubscriptionDisplayViewModel subscription = null;

            try
            {
                subscription = SubscriptionService.Read(id.Value);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // GET: Subscriptions/Create
        public IActionResult Create()
        {
            ViewBag.LevelId = new SelectList(SubscriptionLevelService.ReadAll(), "Id", "Title");
            ViewBag.VideoIds = new SelectList(VideoService.ReadAll(), "Id", nameof(VideoDisplayViewModel.Name));

            return View();
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubscriptionCreateViewModel subscriptionData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SubscriptionService.CreateAsync(subscriptionData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.LevelId = new SelectList(SubscriptionLevelService.ReadAll(), "Id", "Title");
            ViewBag.VideoIds = new SelectList(VideoService.ReadAll(), "Id", nameof(VideoDisplayViewModel.Name));

            return View(subscriptionData);
        }

        // GET: Subscriptions/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = SubscriptionService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (subscription == null)
            {
                return NotFound();
            }

            ViewBag.LevelId = new SelectList(SubscriptionLevelService.ReadAll(), "Id", "Title");
            ViewBag.VideoIds = new SelectList(VideoService.ReadAll(), "Id", nameof(VideoDisplayViewModel.Name));

            var subscriptionEditModel = new SubscriptionEditViewModel()
            {
                Id = subscription.Id,
                Title = subscription.Title,
                Description = subscription.Description,
                Cost = subscription.Cost,
                LevelId = subscription.LevelId,
                ReleasedIn = subscription.ReleasedIn,
                Duration = subscription.Duration,
                AvailableUntil = subscription.AvailableUntil,
                VideoIds = subscription.VideosInSubscription.Select(x => x.VideoId)
            };

            return View(subscriptionEditModel);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, SubscriptionEditViewModel subscriptionNewData)
        {
            if (id != subscriptionNewData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SubscriptionService.Update(id, subscriptionNewData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.LevelId = new SelectList(SubscriptionLevelService.ReadAll(), "Id", "Title");
            ViewBag.VideoIds = new SelectList(VideoService.ReadAll(), "Id", nameof(VideoDisplayViewModel.Name));

            return View(subscriptionNewData);
        }

        // GET: Subscriptions/Delete/5
        public IActionResult Delete(Guid? id)
        {
            var subscription = SubscriptionService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var subscription = SubscriptionService.ReadAll()
                .FirstOrDefault(m => m.Id == id);
            SubscriptionService.Delete(subscription.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(Guid id)
        {
            return SubscriptionService.IsEntityExist(id);
        }
    }
}

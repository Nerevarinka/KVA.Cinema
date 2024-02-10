using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVA.Cinema.Models;
using KVA.Cinema.Models.Entities;
using KVA.Cinema.Services;
using KVA.Cinema.Models.ViewModels.Video;
using Microsoft.AspNetCore.Http;

namespace KVA.Cinema.Controllers
{
    public class VideosController : Controller
    {
        private VideoService VideoService { get; }

        private CountryService CountryService { get; }

        private DirectorService DirectorService { get; }

        private LanguageService LanguageService { get; }

        private PegiService PegiService { get; }

        private GenreService GenreService { get; }

        public VideosController(VideoService videoService,
                                CountryService countryService,
                                DirectorService directorService,
                                LanguageService languageService,
                                PegiService pegiService,
                                GenreService genreService)
        {
            VideoService = videoService;
            CountryService = countryService;
            DirectorService = directorService;
            LanguageService = languageService;
            PegiService = pegiService;
            GenreService = genreService;
        }

        // GET: Videos
        public IActionResult Index()
        {

            var data = VideoService.ReadAll();
            return View(data);
        }

        // GET: Videos/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = VideoService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            ViewBag.CountryId = new SelectList(CountryService.ReadAll(), "Id", "Name");
            ViewBag.DirectorId = new SelectList(DirectorService.ReadAll(), "Id", "Name");
            ViewBag.LanguageId = new SelectList(LanguageService.ReadAll(), "Id", "Name");
            ViewBag.PegiId = new SelectList(PegiService.ReadAll(), "Id", "Type");
            ViewBag.GenresId = new SelectList(GenreService.ReadAll(), "Id", "Title");
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VideoCreateViewModel videoData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    VideoService.CreateAsync(videoData);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            // TODO: При каждой ошибки валидации считывается 5 раз наборы сущностей
            ViewBag.CountryId = new SelectList(CountryService.ReadAll(), "Id", "Name");
            ViewBag.DirectorId = new SelectList(DirectorService.ReadAll(), "Id", "Name");
            ViewBag.LanguageId = new SelectList(LanguageService.ReadAll(), "Id", "Name");
            ViewBag.PegiId = new SelectList(PegiService.ReadAll(), "Id", "Type");
            ViewBag.GenresId = new SelectList(GenreService.ReadAll(), "Id", "Title");

            return View(videoData);
        }

        // GET: Videos/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = VideoService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (video == null)
            {
                return NotFound();
            }

            ViewBag.CountryId = new SelectList(CountryService.ReadAll(), "Id", "Name");
            ViewBag.DirectorId = new SelectList(DirectorService.ReadAll(), "Id", "Name");
            ViewBag.LanguageId = new SelectList(LanguageService.ReadAll(), "Id", "Name");
            ViewBag.PegiId = new SelectList(PegiService.ReadAll(), "Id", "Type");
            ViewBag.GenresId = new SelectList(GenreService.ReadAll(), "Id", "Title");

            var videoEditModel = new VideoEditViewModel()
            {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                Length = video.Length,
                CountryId = video.CountryId,
                ReleasedIn = video.ReleasedIn,
                Views = video.Views,
                Preview = video.Preview,
                PreviewFileName = video.PreviewFileName,
                PegiId = video.PegiId,
                LanguageId = video.LanguageId,
                DirectorId = video.DirectorId,
                //GenresId = video.GenresId
            };

            return View(videoEditModel);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, VideoEditViewModel videoNewData)
        {

            if (id != videoNewData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    VideoService.Update(id, videoNewData);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.CountryId = new SelectList(CountryService.ReadAll(), "Id", "Name");
            ViewBag.DirectorId = new SelectList(DirectorService.ReadAll(), "Id", "Name");
            ViewBag.LanguageId = new SelectList(LanguageService.ReadAll(), "Id", "Name");
            ViewBag.PegiId = new SelectList(PegiService.ReadAll(), "Id", "Type");
            ViewBag.GenresId = new SelectList(GenreService.ReadAll(), "Id", "Title");

            return View(videoNewData);
        }

        // GET: Videos/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = VideoService.ReadAll()
                .FirstOrDefault(m => m.Id == id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var video = VideoService.ReadAll()
                .FirstOrDefault(m => m.Id == id);
            VideoService.Delete(video.Id);

            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(Guid id)
        {
            return VideoService.IsEntityExist(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVA.Cinema.Models;
using KVA.Cinema.Models.Entities;

namespace KVA.Cinema.Controllers
{
    public class PegiController : Controller
    {
        private readonly CinemaContext _context;

        public PegiController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Pegi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pegis.ToListAsync());
        }

        // GET: Pegi/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegi = await _context.Pegis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pegi == null)
            {
                return NotFound();
            }

            return View(pegi);
        }

        // GET: Pegi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pegi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] Pegi pegi)
        {
            if (ModelState.IsValid)
            {
                pegi.Id = Guid.NewGuid();
                _context.Add(pegi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pegi);
        }

        // GET: Pegi/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegi = await _context.Pegis.FindAsync(id);
            if (pegi == null)
            {
                return NotFound();
            }
            return View(pegi);
        }

        // POST: Pegi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type")] Pegi pegi)
        {
            if (id != pegi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pegi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PegiExists(pegi.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pegi);
        }

        // GET: Pegi/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegi = await _context.Pegis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pegi == null)
            {
                return NotFound();
            }

            return View(pegi);
        }

        // POST: Pegi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pegi = await _context.Pegis.FindAsync(id);
            _context.Pegis.Remove(pegi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PegiExists(Guid id)
        {
            return _context.Pegis.Any(e => e.Id == id);
        }
    }
}

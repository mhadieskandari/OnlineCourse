using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PresentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Presents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Presents.Include(p => p.Section).Include(p=>p.Schedules);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Presents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var present = await _context.Presents
                .Include(p => p.Section)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (present == null)
            {
                return NotFound();
            }

            return View(present);
        }

        // GET: Admin/Presents/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id");
            return View();
        }

        // POST: Admin/Presents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,SectionId")] Present present)
        {
            if (ModelState.IsValid)
            {
                _context.Add(present);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", present.SectionId);
            return View(present);
        }

        // GET: Admin/Presents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var present = await _context.Presents.SingleOrDefaultAsync(m => m.Id == id);
            if (present == null)
            {
                return NotFound();
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", present.SectionId);
            return View(present);
        }

        // POST: Admin/Presents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,SectionId")] Present present)
        {
            if (id != present.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(present);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentExists(present.Id))
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
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Id", present.SectionId);
            return View(present);
        }

        // GET: Admin/Presents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var present = await _context.Presents
                .Include(p => p.Section)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (present == null)
            {
                return NotFound();
            }

            return View(present);
        }

        // POST: Admin/Presents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var present = await _context.Presents.SingleOrDefaultAsync(m => m.Id == id);
            _context.Presents.Remove(present);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresentExists(int id)
        {
            return _context.Presents.Any(e => e.Id == id);
        }
    }
}

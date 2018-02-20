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
    public class Sections1Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Sections1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Sections1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sections.Include(s => s.Course).Include(s => s.Teacher).Include(s => s.Term);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Sections1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(s => s.Course)
                .Include(s => s.Teacher)
                .Include(s => s.Term)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Admin/Sections1/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName");
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Description");
            return View();
        }

        // POST: Admin/Sections1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalTime,HourlyPrice,CourseId,TermId,TeacherId,Activity")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", section.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName", section.TeacherId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Description", section.TermId);
            return View(section);
        }

        // GET: Admin/Sections1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.SingleOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", section.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName", section.TeacherId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Description", section.TermId);
            return View(section);
        }

        // POST: Admin/Sections1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalTime,HourlyPrice,CourseId,TermId,TeacherId,Activity")] Section section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", section.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName", section.TeacherId);
            ViewData["TermId"] = new SelectList(_context.Terms, "Id", "Description", section.TermId);
            return View(section);
        }

        // GET: Admin/Sections1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .Include(s => s.Course)
                .Include(s => s.Teacher)
                .Include(s => s.Term)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Admin/Sections1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Sections.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}

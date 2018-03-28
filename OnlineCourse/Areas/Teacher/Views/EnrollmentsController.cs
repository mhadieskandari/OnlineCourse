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
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Enrollments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enrollments.Include(e => e.Present).Include(e => e.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Present)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Admin/Enrollments/Create
        public IActionResult Create()
        {
            ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Markdown,PresentId,Activity,StudentId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id", enrollment.PresentId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id", enrollment.PresentId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // POST: Admin/Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Markdown,PresentId,Activity,StudentId")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
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
            ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id", enrollment.PresentId);
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Admin/Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Present)
                .Include(e => e.Student)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Admin/Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}

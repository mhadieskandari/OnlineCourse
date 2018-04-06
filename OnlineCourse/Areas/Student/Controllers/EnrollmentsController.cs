using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "0,1,10")]
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrentUser _user;
        private readonly IServiceProvider _provider;
        private readonly HistoryService _historyService;
        private readonly MessageService _msgSender;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EnrollmentsController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _user = user;
            _provider = provider;
            _historyService = historyService;
            _msgSender = new MessageService();
            _hostingEnvironment = hostingEnvironment;
            
        }

        // GET: Student/Enrollments
        public async Task<IActionResult> Index()
        {
            var userid = (await _user.GetUser()).Id;
            var enrols = _context.Enrollments.Where(e => e.StudentId == userid).Include(e => e.Present).ThenInclude(p=>p.Section).Include(e => e.Student);
            return View(await enrols.ToListAsync());
        }

        // GET: Student/Enrollments/Details/5
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

        // GET: Student/Enrollments/Create
        public IActionResult Create()
        {
            ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id");
            //var degree = await _user.GetUserDegree();
            //    && e.Present.Section.Course.Level == degree
            return View();
        }

        // POST: Student/Enrollments/Create
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

        // GET: Student/Enrollments/Edit/5
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

        // POST: Student/Enrollments/Edit/5
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

        // GET: Student/Enrollments/Delete/5
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

        // POST: Student/Enrollments/Delete/5
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

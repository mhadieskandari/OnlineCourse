using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "0,1,10")]
    public class EnrollmentsController : BaseController
    {
        // GET: Student/Enrollments
        public EnrollmentsController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor)
        {
        }

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
        public async Task<IActionResult> SelectCourse()
        {
            //ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id");
            //ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id");


            var degree = await _user.GetUserDegree();
            if (degree == null)
            {
                this.AddNotification("لطفا مقطع تحصیلی خود را وارد کنید و مجددا تلاش کنید.",NotificationType.Error);
                return RedirectToAction("Index", "Profile");
            }
            var model = _context.Presents.Where(p => p.Section.Course.Level == degree &&
                                                     !_context.Enrollments.Any(e => e.PresentId == p.Id && e.Present.Section.Activity==ActiveState.Active))
                                                     .Include(p=>p.Section).ThenInclude(p=>p.Course)
                                                     .Include(p=>p.Section).ThenInclude(p=>p.Teacher)
                                                     .Include(p=>p.Schedules);
            
            return View(model);
        }

        // POST: Student/Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectCourse([Bind("Id,Markdown,PresentId,Activity,StudentId")] Enrollment enrollment)
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

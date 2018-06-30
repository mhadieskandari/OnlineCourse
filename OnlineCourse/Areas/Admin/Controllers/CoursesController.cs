using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Panel.Areas.Student.Controllers;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "10")]
    public class CoursesController : BaseController
    {
        public CoursesController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, PublicConfig config) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor, config)
        {
        }

        // GET: Admin/Courses
        public async Task<IActionResult> Index(string Name, byte? Level)
        {
            try
            {

                var model = _context.Courses.Where(c => c.Id > 0);
                if (!string.IsNullOrEmpty(Name))
                {
                    model = model.Where(c => c.Name.Contains(Name));
                }
                if (Level.HasValue)
                {
                    model = model.Where(c => c.Level == (EducationLevel)Level);
                }
                return View(await model.ToListAsync());
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        // GET: Admin/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (course == null)
                {
                    return NotFound();
                }

                return View(course);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }

        }

        // GET: Admin/Courses/Create
        public IActionResult Create()
        {
            try
            {

                return View();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExistCourse = await _context.Courses.AnyAsync(c => c.Name == course.Name && c.Level == course.Level);
                    if (isExistCourse)
                    {
                        this.AddNotification("این درس قبلا ایجاد شده است.", NotificationType.Error);
                        return View(course);
                    }
                    _context.Add(course);
                    await _context.SaveChangesAsync();
                    this.AddNotification("درس با موفقیت ایجاد شد.", NotificationType.Success);
                    return RedirectToAction(nameof(Index));
                }
                return View(course);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        // GET: Admin/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
                if (course == null)
                {
                    return NotFound();
                }
                return View(course);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }

        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Level")] Course course)
        {
            try
            {
                if (id != course.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(course);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CourseExists(course.Id))
                        {
                            return NotFound();
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(course);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }

        }

        // GET: Admin/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (course == null)
                {
                    return NotFound();
                }

                return View(course);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage", "Home");
            }

        }

        private bool CourseExists(int id)
        {
            try
            {
                return _context.Courses.Any(e => e.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                return true;
            }
        }

    }
}

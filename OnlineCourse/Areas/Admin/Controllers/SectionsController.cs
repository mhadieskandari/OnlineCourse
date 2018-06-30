using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using OnlineCourse.Panel.Utils.ViewModels.Areas.Admin;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "10")]
    public class SectionsController : BaseController
    {
        private readonly IMapper _mapper;

        public SectionsController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, PublicConfig config,IMapper mapper) : 
            base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor, config)
        {
            _mapper = mapper;
        }

        // GET: Admin/Sections
        public async Task<IActionResult> Index()
        {
            try
            {

                return View(await _context.Sections.Include(t => t.Term).Include(c => c.Course).Include(u => u.Teacher).ToListAsync());
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // GET: Admin/Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var section = await _context.Sections.Include(s => s.Presents).ThenInclude(p => p.Schedules).Include(s => s.Course).Include(s => s.Teacher).Include(s => s.Term)
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (section == null)
                {
                    return NotFound();
                }

                return View(section);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // GET: Admin/Sections/Create
        public IActionResult Create()
        {
            try
            {
                var model = new SectionCreateViewModel(_context);
                return View(model);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // POST: Admin/Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SectionCreateViewModel section)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbSection = _mapper.Map<Section>(section);

                    var isExist =
                        _context.Sections.Any(s => s.CourseId == dbSection.CourseId &&
                                                   s.TeacherId == dbSection.TeacherId &&
                                                   s.TermId == dbSection.TermId);

                    if (isExist)
                    {
                        this.AddNotification("این دوره وجود دارد و امکان ایجاد ندارد.", NotificationType.Info);
                        return View(section);
                    }

                    _context.Add(dbSection);

                    await _context.SaveChangesAsync();
                    var present = new Present() { SectionId = dbSection.Id };
                    _context.Presents.Add(present);

                    await _context.SaveChangesAsync();
                    var scheduls = section.WorkDays.Split(",");
                    var startTime = TimeSpan.Parse(section.StartTime);
                    var endTime = TimeSpan.Parse(section.EndTime);
                    foreach (var s in scheduls)
                    {
                        _context.Schedules.Add(new Schedule() { DayOfWeek = (WeekDays)int.Parse(s), StartTime = startTime, EndTime = endTime, PresentId = present.Id });
                    }
                    await _context.SaveChangesAsync();
                    this.AddNotification("دوره با موفقیت ایجاد شد.", NotificationType.Success);
                    return RedirectToAction(nameof(Details), new { id = dbSection.Id });
                }
                return View(section);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSchedul(Schedule schedule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Schedules.Add(schedule);

                    await _context.SaveChangesAsync();
                    this.AddNotification("روز با موفقیت به دوره افزوده شد.", NotificationType.Success);
                    var present = _context.Presents.FirstOrDefault(s => s.Id == schedule.PresentId);
                    if (present != null)
                    {
                        return RedirectToAction(nameof(Details), new { Id = present.SectionId });
                    }
                    _historyService.LogError(new Exception("addSchedul in section has error"), HistoryErrorType.Middle);
                    return RedirectToAction(nameof(Index));

                }
                this.AddNotification("خطایی در ایجاد روز رخ داده است.", NotificationType.Error);
                return View(nameof(Index));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPresent(AddPresentScedulViewModel present)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = new Present() { SectionId = present.SectionId };
                    _context.Presents.Add(p);
                    await _context.SaveChangesAsync();

                    var scheduls = present.WorkDays.Split(",");
                    var startTime = TimeSpan.Parse(present.StartTime);
                    var endTime = TimeSpan.Parse(present.EndTime);
                    foreach (var s in scheduls)
                    {
                        _context.Schedules.Add(new Schedule() { DayOfWeek = (WeekDays)int.Parse(s), StartTime = startTime, EndTime = endTime, PresentId = p.Id });
                    }
                    await _context.SaveChangesAsync();
                    this.AddNotification("برنامه با موفقیت به دوره افزوده شد.", NotificationType.Success);
                    return RedirectToAction(nameof(Details), new { id = present.SectionId });

                }
                this.AddNotification("خطایی در ایجاد روز رخ داده است.", NotificationType.Error);
                return View(nameof(Index));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }
        // GET: Admin/Sections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
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
                var model = _mapper.Map<SectionEditViewModel>(section);
                model.IsEdit(_context);
                return View(model);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }
        }

        // POST: Admin/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SectionEditViewModel section)
        {
            try
            {

                if (id != section.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var dbSection = _mapper.Map<Section>(section);
                        _context.Update(dbSection);
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
                return View(section);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }
        }

        // GET: Admin/Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var section = await _context.Sections
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (section == null)
                {
                    return NotFound();
                }

                return View(section);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // POST: Admin/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var section = await _context.Sections.SingleOrDefaultAsync(m => m.Id == id);
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }
        }

        // POST: Admin/Sections/DeleteSchedul/5
        [HttpPost, ActionName("DeleteSchedul")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSchedulConfirmed(int schedulid)
        {
            try
            {
                var schedule = await _context.Schedules.SingleOrDefaultAsync(m => m.Id == schedulid);
                var presentId = schedule.PresentId;
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
                this.AddNotification("این روز با موفقیت حذف شد.", NotificationType.Success);
                var present = _context.Presents.FirstOrDefault(s => s.Id == presentId);
                if (present != null)
                {
                    return RedirectToAction(nameof(Details), new { Id = present.SectionId });
                }
                _historyService.LogError(new Exception("addSchedul in section has error"), HistoryErrorType.Middle);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // POST: Admin/Sections/DeleteSchedul/5
        [HttpPost, ActionName("DeletePresent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePresentConfirmed(int presentid)
        {
            try
            {
                var present = await _context.Presents.Include(p => p.Section).SingleOrDefaultAsync(m => m.Id == presentid);
                var sectionid = present.SectionId;
                _context.Presents.Remove(present);
                await _context.SaveChangesAsync();
                this.AddNotification("این برنامه با موفقیت حذف شد.", NotificationType.Success);
                return RedirectToAction(nameof(Details), new { Id = sectionid });
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        private bool SectionExists(int id)
        {
            try
            {

                return _context.Sections.Any(e => e.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                return false;
            }
        }
    }
}

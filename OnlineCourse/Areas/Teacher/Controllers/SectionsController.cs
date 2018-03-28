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
using OnlineCourse.Core.Services;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "10,1")]
    public class SectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly HistoryService _history;
        private readonly CurrentUser _user;
        private readonly int _userid;

        public SectionsController(ApplicationDbContext context, IMapper mapper, HistoryService history, CurrentUser user)
        {
            _context = context;
            _mapper = mapper;
            _history = history;
            _user = user;
            _userid = _user.GetUserId().Result.Value;
        }

        // GET: Admin/Sections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.Include(t => t.Term).Include(c => c.Course).Include(u => u.Teacher).Where(s => s.TeacherId == _userid).ToListAsync());
        }

        // GET: Admin/Sections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.Include(s => s.Presents).ThenInclude(p => p.Schedules).Include(s => s.Course).Include(s => s.Teacher).Include(s => s.Term)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (section == null || section.TeacherId != _userid)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Admin/Sections/Create
        public IActionResult Create()
        {
            var model = new SectionViewModel(_context);
            model.TeacherId = _userid;
            return View(model);
        }

        // POST: Admin/Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SectionViewModel section)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (section.TeacherId != _userid)
                    {
                        return NotFound();
                    }

                    var dbSection = _mapper.Map<Section>(section);
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
                    return RedirectToAction(nameof(Details));
                }
                return View(section);
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطایی در ایجاد دوره رخ داده است.", NotificationType.Error);
                throw;
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
                    var isOwner = _context.Sections.Any(s => s.Presents.Any(p=>p.Id==schedule.PresentId) && s.TeacherId==_userid );
                    if (!isOwner)
                        return NotFound();

                    _context.Schedules.Add(schedule);
                    await _context.SaveChangesAsync();
                    this.AddNotification("روز با موفقیت به برنامه افزوده شد.", NotificationType.Success);
                    var present = _context.Presents.FirstOrDefault(s => s.Id == schedule.PresentId);
                    if (present != null)
                    {
                        return RedirectToAction(nameof(Details), new { Id = present.SectionId });
                    }
                    _history.LogError(new Exception("addSchedul in section has error"), HistoryErrorType.Middle);
                    return RedirectToAction(nameof(Index));
                }
                this.AddNotification("خطایی در ایجاد روز رخ داده است.", NotificationType.Error);
                return View(nameof(Index));
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطایی در ایجاد روز رخ داده است.", NotificationType.Error);
                throw;
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
                this.AddNotification("خطایی در ایجاد برنامه رخ داده است.", NotificationType.Error);
                return View(nameof(Index));
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطایی در ایجاد برنامه رخ داده است.", NotificationType.Error);
                throw;
            }

        }
        // GET: Admin/Sections/Edit/5
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
            var model = _mapper.Map<SectionViewModel>(section);
            model.IsEdit(_context);
            return View(model);
        }

        // POST: Admin/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SectionViewModel section)
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

        // GET: Admin/Sections/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var section = await _context.Sections.SingleOrDefaultAsync(m => m.Id == id);
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
                _history.LogError(new Exception("addSchedul in section has error"), HistoryErrorType.Middle);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                throw;
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
                _history.LogError(e, HistoryErrorType.Middle);
                throw;
            }

        }

        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }
    }
}

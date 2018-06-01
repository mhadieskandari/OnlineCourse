using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using OnlineCourse.Panel.Utils.ViewModels.Areas.Admin;
using AutoMapper;
using BigBlueButton;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using OnlineCourse.Core.Services;
using OnlineCourse.Panel.Utils.Extentions;
using Microsoft.Extensions.Configuration;
using OnlineCourse.Core;
using OnlineCourse.Core.Extentions;

namespace OnlineCourse.Panel.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "1")]
    public class SectionsController : BaseController
    {
        private readonly int _userid;
        public SectionsController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IMapper mapper, PublicConfig config) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor, mapper, config)
        {
            _userid = user.GetUserId().Result;
        }
        // GET: Admin/Sections
        public async Task<IActionResult> Index(int? termId, int? courseId, ActiveState? activity)
        {
            var model = _context.Sections.Include(t => t.Term).Include(c => c.Course).Include(u => u.Teacher).Include(s=>s.Presents)
                .Where(s => s.TeacherId == _userid);
            if (termId.HasValue)
                model = model.Where(s => s.TermId == termId);
            if (courseId.HasValue)
                model = model.Where(s => s.CourseId == courseId);
            if (activity.HasValue)
                model = model.Where(s => s.Activity == activity);
            return View(await model.ToListAsync());
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
            var model = new SectionCreateViewModel(_context);
            model.TeacherId = _userid;
            return View(model);
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
                    if (section.TeacherId != _userid)
                    {
                        return NotFound();
                    }

                    var dbSection = _mapper.Map<Section>(section);


                    var existSecrion = _context.Sections.FirstOrDefault(s => s.CourseId == dbSection.CourseId &&
                                                    s.TeacherId == dbSection.TeacherId &&
                                                    s.TermId == dbSection.TermId);

                    if (existSecrion != null)
                    {
                        this.AddNotification("این دوره با id =" + existSecrion.Id + " وجود دارد و امکان ایجاد ندارد.", NotificationType.Info);
                        section.IsEdit(_context);
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
                this.AddNotification("خطایی رخ داده است.", NotificationType.Error);
                section.IsEdit(_context);
                return View(section);
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطایی در ایجاد دوره رخ داده است.", NotificationType.Error);
                return RedirectToAction("Error", "Home");
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
                    var isOwner = _context.Sections.Any(s => s.Presents.Any(p => p.Id == schedule.PresentId) && s.TeacherId == _userid);
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
                return RedirectToAction("Error", "Home");
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
            var model = _mapper.Map<SectionEditViewModel>(section);
            model.IsEdit(_context);
            return View(model);
        }

        // POST: Admin/Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SectionEditViewModel section)
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
                _history.LogError(e, HistoryErrorType.Middle);
                return RedirectToAction("Error", "Home");
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
                _history.LogError(e, HistoryErrorType.Middle);
                return RedirectToAction("Error", "Home");
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
                _history.LogError(new Exception("addSchedul in section has error"), HistoryErrorType.Middle);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }

        }



        private bool SectionExists(int id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }

        public IActionResult PresentDetails(int presentId)
        {
            try
            {
                var classRooms = _context.ClassRooms.Where(c => c.PresentId == presentId && c.Present.Section.TeacherId == _userid).OrderByDescending(c => c.Id).ToList();
               
                return View(classRooms);
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                return RedirectToAction("Error", "Home");
            }

        }


        //[HttpPost]
        public IActionResult CreateClass(int presentId)
        {
            try
            {
                var present = _context.Presents.SingleOrDefault(p => p.Section.TeacherId == _userid && p.Id == presentId);

                if (present == null)
                {
                    return NotFound();
                }
                var cls = new ClassRoom()
                {
                    ChangeTimePermit = 10,
                    Date = DateTime.Now,
                    StartedTime = DateTime.Now.TimeOfDay,
                    PresentId = present.Id,
                    Status = ClassStatus.NotStarted,
                };
                _context.ClassRooms.Add(cls);
                _context.SaveChanges();


                return RedirectToAction(nameof(PresentDetails),new {presentId});
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطا در ایجاد جلسه", NotificationType.Error);
                return RedirectToAction(nameof(Index));
            }
        }


        //[HttpPost]
        public IActionResult JoinClass(int classid)
        {
            try
            {
                var classroom = _context.ClassRooms.Include(c => c.Present).ThenInclude(p => p.Section).ThenInclude(s => s.Course).AsNoTracking().SingleOrDefault(c => c.Id == classid && c.Present.Section.TeacherId == _userid);

                if (classroom == null)
                {
                    this.AddNotification("کلاسی با مشخصات فوق یافت نشد.", NotificationType.Error);
                    return RedirectToAction(nameof(Index));
                }


                var moderatorPwd = _config.BbbGetModeratorPassword();
                var attendePwd = classroom.Id + "_" + classroom.PresentId + "_" + classroom.Present.Section.TeacherId;
                var bbb = new BBB();

                var request = _httpContextAccessor.HttpContext.Request;
                var uriBuilder = new UriBuilder();
                uriBuilder.Scheme = request.Scheme;
                uriBuilder.Host = request.Host.Host;
                if (request.Host.Port != null) uriBuilder.Port = request.Host.Port.Value;
                uriBuilder.Path = "Teacher/Sections/PresentDetails";
                uriBuilder.Query = "presentId=" + classroom.PresentId;

                var createResult = bbb.CreateMeeting(_user.GetEmail(), classroom.Id.ToString(), attendePwd, moderatorPwd, uriBuilder.ToString(),"").Rows[0];

                if (createResult != null && createResult[0].ToString().ToLower() == "SUCCESS".ToLower())
                {
                    //classroom.Status = ClassStatus.OnGoing;
                    //_context.SaveChanges();

                    uriBuilder.Path = "api/BigBlueButtonHooks/index";
                    uriBuilder.Query = "meetingid=" + classroom.Id;

                    var callbackurl = uriBuilder.ToString();
                    var hookres = bbb.CreateHooks(callbackurl/*, classroom.Id.ToString()*/).Rows[0];
                    var url = bbb.JoinMeeting(_user.GetEmail(), classroom.Id.ToString(), moderatorPwd, true);
                    return Redirect(url);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطا در ایجاد جلسه", NotificationType.Error);
                return RedirectToAction(nameof(Index));
            }
        }
        //public IActionResult CreateHook(string callbackurl)
        //{
        //    try
        //    {

        //        var bbb = new BBB();
        //        var hooksResponse = bbb.CreateHooks(callbackurl);


        //        return Redirect(hooksResponse);
        //    }
        //    catch (Exception e)
        //    {
        //        _history.LogError(e, HistoryErrorType.Middle);
        //        this.AddNotification("خطا در ایجاد جلسه", NotificationType.Error);
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        public IActionResult HookList()
        {
            try
            {

                var bbb = new BBB();
                var hooksResponse = bbb.HooksList();


                return Redirect(hooksResponse);
            }
            catch (Exception e)
            {
                _history.LogError(e, HistoryErrorType.Middle);
                this.AddNotification("خطا در ایجاد جلسه", NotificationType.Error);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

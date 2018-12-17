using System;
using System.Linq;
using System.Threading.Tasks;
using BigBlueButton;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core;
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
        private readonly int _userId;
        // GET: Student/Enrollments
        public EnrollmentsController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, PublicConfig config) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor, config)
        {
            _userId = _user.GetUserId().Result;
        }

        public async Task<IActionResult> Index()
        {
            var enrols = _context.Enrollments.Where(e => e.StudentId == _userId)
                    .Include(e => e.Present).ThenInclude(p => p.Section).ThenInclude(s => s.Course)
                    .Include(e => e.Present).ThenInclude(p => p.Schedules)
                    .Include(e => e.Student)
                    .Include(e => e.Payments);
            return View(await enrols.ToListAsync());

        }

        // GET: Student/Enrollments/Create
        public async Task<IActionResult> SelectCourse(int? enrollmentid)
        {
            //ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id");
            //ViewData["StudentId"] = new SelectList(_context.Users, "Id", "Id");


            var degree = await _user.GetUserDegree();
            if (degree == null)
            {
                this.AddNotification("لطفا مقطع تحصیلی خود را وارد کنید و مجددا تلاش کنید.", NotificationType.Error);
                return RedirectToAction("Index", "Profile");
            }
            var model = _context.Presents.Where(p => p.Section.Course.Level == degree /*&& !_context.Enrollments.Any(e => e.PresentId == p.Id && e.Present.Section.Activity==ActiveState.Active)*/)
                                                     .Include(p => p.Section).ThenInclude(p => p.Course)
                                                     .Include(p => p.Section).ThenInclude(p => p.Teacher)
                                                     .Include(p => p.Schedules)
                                                     .Include(p => p.Enrollments).ThenInclude(e => e.Payments).AsQueryable();
            if (enrollmentid.HasValue)
                model = model.Where(p => p.Enrollments.Any(e => e.Id == enrollmentid.Value));

            return View(model.OrderByDescending(p => p.Enrollments.Any()));
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



        // GET: Student/Enrollments/Create
        public IActionResult Details(int enrollmentId)
        {
            try
            {
                var model = _context.ClassRooms.Include(c => c.Present).ThenInclude(p => p.Enrollments).Where(c => c.Present.Enrollments.Any(e => e.Id == enrollmentId)).AsNoTracking().ToList();
                return View(model);
            }
            catch (Exception e)
            {
               _historyService.LogError(e,HistoryErrorType.Middle);
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Student/Enrollments/Create
        public async Task<IActionResult> JoinCourse(int CourseId)
        {
            var classRoom =_context.ClassRooms.Include(c=>c.Present).ThenInclude(p=>p.Section).AsNoTracking().FirstOrDefault(c => c.Id==CourseId);
            if (classRoom == null)
                return RedirectToAction("NotFoundPage", "Home");
            
            var moderatorPwd = _config.BbbGetModeratorPassword();
            var attendePwd = classRoom.Id + "_" + classRoom.PresentId + "_" + classRoom.Present.Section.TeacherId;
            var bbb = new BBB(_config.BbbGetServerIpAddress(), _config.BbbGetServerId());
            var createResult = bbb.IsMeetingRunning(classRoom.Id.ToString()).Rows[0];

            if (createResult != null && createResult[0].ToString().ToLower() == "success" /*&& bool.Parse(createResult[1].ToString())*/)
            {
                var url = bbb.JoinMeeting(/*classroom.Present.Section.Course.CourseName*/ _user.GetEmail(), classRoom.Id.ToString(), attendePwd, true, true);
                return Redirect(url);
            }

            return View();
        }


    }

}

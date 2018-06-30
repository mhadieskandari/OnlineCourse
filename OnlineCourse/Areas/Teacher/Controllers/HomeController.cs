using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "1")]
    public class HomeController : BaseController
    {
        private readonly int _userid;
       public HomeController(ApplicationDbContext context, CurrentUser user, HistoryService history, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IMapper mapper, PublicConfig config) : base(context, user, history, provider, hostingEnvironment, httpContextAccessor, mapper, config)
       {
           _userid = _user.GetUserId().Result;
       }

        public IActionResult Index()
        {
            try
            {
                var courses = _context.Presents.Include(p => p.Section ).ThenInclude(s => s.Course).AsNoTracking().Where(p=> p.Section.TeacherId == _userid).ToList();
                return View(courses);
            }
            catch (Exception e)
            {
                _history.LogError(e,HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده",NotificationType.Error);
                return RedirectToAction("ErrorPage");
            }
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }
        public IActionResult ErrorPage()
        {
            return View();
        }

    }
}
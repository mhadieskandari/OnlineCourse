using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core;
using OnlineCourse.Core.Dtos;
using OnlineCourse.Core.Extentions;
using OnlineCourse.Core.Services;
using OnlineCourse.Core.WorkFlows;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using OnlineCourse.Panel.Utils;
using OnlineCourse.Panel.Utils.Extentions;
using OnlineCourse.Panel.Utils.ViewModels.AccountViewModels;
using SQLitePCL;

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
                Console.WriteLine(e);
                throw;
            }
        }


        public IActionResult NotFoundPage()
        {
            return View();
        }

    }
}
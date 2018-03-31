using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "0")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CurrentUser _user;

        private readonly IServiceProvider _provider;
        private readonly HistoryService _historyService;
        private readonly MessageService _msgSender;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _user = user;
            _provider = provider;
            _historyService = historyService;
            _msgSender = new MessageService();
            _hostingEnvironment = hostingEnvironment;

        }

        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult NotFoundPage()
        {
            return View();
        }

       


    }
}
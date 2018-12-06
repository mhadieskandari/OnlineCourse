using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly CurrentUser _user;
        public readonly IServiceProvider _provider;
        public readonly HistoryService _historyService;
        public readonly MessageService _msgSender;
        public readonly IHostingEnvironment _hostingEnvironment;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly PublicConfig _config;

        public BaseController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, PublicConfig config)
        {

            _context = context;
            _user = user;
            _provider = provider;
            _historyService = historyService;
            _msgSender = new MessageService();
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }
    }
}
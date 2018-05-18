using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Api.Controllers
{
    public class BaseController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly CurrentUser _user;
        public readonly IServiceProvider _provider;
        public readonly HistoryService _history;
        public readonly MessageService _msgSender;
        public readonly IHostingEnvironment _hostingEnvironment;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IMapper _mapper;
        public readonly PublicConfig _config;

        public BaseController(ApplicationDbContext context, CurrentUser user, HistoryService history, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IMapper mapper, PublicConfig config)
        {
            _context = context;
            _user = user;
            _provider = provider;
            _history = history;
            _msgSender = new MessageService();
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _config = config;
        }
    }
}
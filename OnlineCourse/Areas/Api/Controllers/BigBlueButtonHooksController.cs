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
    [Area("Api")]
    public class BigBlueButtonHooksController : BaseController
    {
        public BigBlueButtonHooksController(ApplicationDbContext context, CurrentUser user, HistoryService history, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, IMapper mapper, PublicConfig config) : base(context, user, history, provider, hostingEnvironment, httpContextAccessor, mapper, config)
        {
        }
        public IActionResult index()
        {
            var req = _httpContextAccessor.HttpContext.Request;
            var ret = 123123;

            return Json(ret);
        }
        public IActionResult CallBack(string callback)
        {
            var ret = callback;

            return Json(ret);
        }
    }
}
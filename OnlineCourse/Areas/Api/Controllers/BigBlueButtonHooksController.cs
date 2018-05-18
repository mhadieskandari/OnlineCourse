using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineCourse.Panel.Areas.Api.Controllers
{
    [Area("Api")]
    public class BigBlueButtonHooksController : Controller
    {
        public IActionResult index()
        {
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
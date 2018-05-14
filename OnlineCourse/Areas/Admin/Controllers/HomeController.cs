using BigBlueButton;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "10")]
    public class HomeController : Controller
    {
        private readonly BBB _bbb;
        public HomeController()
        {
            _bbb=new BBB();
        }

        public IActionResult Index()
        {
            var b = _bbb.getMeetings();
            var rows = b.Rows[0];

            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }


    }
}
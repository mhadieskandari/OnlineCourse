using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(UserAccessLevel.Administrator))]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            //return RedirectToAction("Index","InvoiceList");
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }


    }
}
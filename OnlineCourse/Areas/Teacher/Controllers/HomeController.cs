using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Core;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles = "10,1")]
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
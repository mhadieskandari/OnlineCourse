using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Panel.Utils.Extentions;
using OnlineCourse.Panel.Utils.ViewModels;

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "0,1,10")]
    public class CheckOutController : BaseController
    {
        public CheckOutController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor)
        {
        }

        // Post: Student/Enrollments/Create
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var shoppingCartCookie = _httpContextAccessor.HttpContext.Request.Cookies["Cart"];
            if (!string.IsNullOrEmpty(shoppingCartCookie))
            {
                byte[] decodedBytes = Convert.FromBase64String(shoppingCartCookie);
                var decodedTxt = System.Text.Encoding.UTF8.GetString(decodedBytes);
                if (string.IsNullOrEmpty(decodedTxt))
                {
                    return Redirect("/");
                }
                var invoiceModel = JsonConvert.DeserializeObject<List<OrderDetail>>(decodedTxt);

                var degree = _user.GetUserDegree().Result;
                if (degree == null)
                {
                    this.AddNotification("لطفا مقطع تحصیلی خود را وارد کنید و مجددا تلاش کنید.", NotificationType.Error);
                    return RedirectToAction("Index", "Profile");
                }
                if (invoiceModel != null || invoiceModel.Any())
                {
                    var viewModel = new List<OrderDetailViewModel>();
                    foreach (var detail in invoiceModel)
                    {
                        var model =await _context.Presents.Include(p => p.Section).ThenInclude(p => p.Course)
                            .Include(p => p.Section).ThenInclude(p => p.Teacher)
                            .Include(p => p.Schedules).SingleOrDefaultAsync(p => p.Id == detail.id);

                        if (model != null)
                        {
                            var od = new OrderDetailViewModel() { 
                                id = detail.id,
                                amount = detail.amount,
                                coursename = model.Section.Course.CourseName,
                                teachername = model.Section.Teacher.FullName,
                                totalcost = model.Section.HourlyPrice * model.Section.TotalTime,
                            };
                            od.remincost = od.totalcost - od.amount;
                            viewModel.Add(od);
                        }
                    }
                    return View(viewModel);
                }
            }
            this.AddNotification("صورت حسابی وجود ندارد.",NotificationType.Error);
            return RedirectToAction("SelectCourse","Enrollments");
        }

    }

}
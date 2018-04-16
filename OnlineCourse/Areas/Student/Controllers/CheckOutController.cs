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
using OnlineCourse.Core.Extentions;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
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
                        var model = await _context.Presents.Include(p => p.Section).ThenInclude(p => p.Course)
                            .Include(p => p.Section).ThenInclude(p => p.Teacher)
                            .Include(p => p.Schedules).SingleOrDefaultAsync(p => p.Id == detail.id);

                        if (model != null)
                        {
                            var od = new OrderDetailViewModel()
                            {
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
            this.AddNotification("صورت حسابی وجود ندارد.", NotificationType.Error);
            return RedirectToAction("SelectCourse", "Enrollments");
        }

        // Post: Student/Enrollments/Create
        [HttpPost]
        public async Task<IActionResult> CreateInvoice(PayType payType = PayType.Online, BankId bankId = BankId.Mellat)
        {
            var userId = await _user.GetUserId();

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
                    var invoice = new Invoice()
                    {
                        LastModifieDateTime = DateTime.Now,
                        Ip = WebHelper.GetRemoteIP,
                        PayState = PayState.Pending,
                        State = GeneralState.Enable,
                        PayType = payType,
                        BankId = bankId
                    };
                    _context.Invoices.Add(invoice);
                    await _context.SaveChangesAsync();

                    var enrolments = new List<Enrollment>();
                    foreach (var detail in invoiceModel)
                    {
                        var present = await _context.Presents.SingleOrDefaultAsync(p => p.Id == detail.id);

                        if (present != null)
                        {
                            enrolments.Add(new Enrollment() { Activity = ActiveState.DeActive, InvoiceId = invoice.Id, PresentId = present.Id, StudentId = userId.Value, });
                        }
                    }
                    _context.Enrollments.AddRange(enrolments);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Payment), new { invoiceid = invoice.Id });
                }
            }
            this.AddNotification("صورت حسابی وجود ندارد.", NotificationType.Error);
            return RedirectToAction("SelectCourse", "Enrollments");
        }

        [HttpGet]
        public async Task<IActionResult> Payment(int invoiceId)
        {
            //var invoice =await _context.Invoices.Include(i=>i.Enrollments).ThenInclude(e=>e.Present).ThenInclude(p=>p.Section).FirstOrDefaultAsync(i => i.Id == invoiceId);

            decimal sumOfPrice = 0;

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
                if (invoiceModel != null)
                {
                    foreach (var detail in invoiceModel)
                    {
                        //sum of amount for all selected course to sendto payment gateway of selected bank
                        sumOfPrice += detail.amount;
                    }
                }
            }
            //todo call bank gateway to payment action
            return RedirectToAction(nameof(Callback), new { invoiceId = invoiceId, transactionId = "sadflellskdl23lkj23kjlklasl312lk", state = 1 });
        }

        [HttpGet]
        public IActionResult Callback(int invoiceId, string transactionId, PayState state)
        {
            var invoice = _context.Invoices.FirstOrDefault(i => i.Id == invoiceId);
            if (invoice != null)
            {
                //todo check payment gatewaye to confirm payment

                invoice.TransactionId = transactionId;
                invoice.PayState = state;
                _context.Update(invoice);
                _context.SaveChanges();

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
                    if (invoiceModel != null)
                    {
                        foreach (var detail in invoiceModel)
                        {
                            var enrollment = _context.Enrollments.SingleOrDefault(e => e.InvoiceId == invoiceId && e.PresentId == detail.id);

                            if (enrollment != null)
                            {
                                var payment = new Payment()
                                {
                                    Amount = detail.amount,
                                    EnrollmentId = enrollment.Id,
                                };
                                _context.Payments.Add(payment);
                                _context.SaveChanges();
                            }
                        }
                    }
                }
            }
            return RedirectToAction(nameof(Result), new { invoiceid = invoiceId });
        }

        [HttpGet]
        public async Task<IActionResult> Result(int? invoiceId)
        {
            var invoice = await _context.Invoices.Include(i => i.Enrollments).ThenInclude(e => e.Payments)/*ThenInclude(e => e.Present).ThenInclude(p => p.Section)*/.FirstOrDefaultAsync(i => i.Id == invoiceId);
            if (invoice.PayState == PayState.Approved)
            {
                this.AddNotification("پرداخت با موفقیت انجام شد.", NotificationType.Success);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("Cart", "[]",new CookieOptions(){Expires = DateTime.Now.AddDays(-30)});
            }
            else
            {
                this.AddNotification("خطایی در پرداخت رخ داده است , در صورتی که مبلغ از حساب شما کسر شده و تا مدت 72 ساعت به حساب شما باز گردانده نشد با پشتیبانی تماس بگیرید. ", NotificationType.Error);
            }
            return View(invoice);
        }
    }

}
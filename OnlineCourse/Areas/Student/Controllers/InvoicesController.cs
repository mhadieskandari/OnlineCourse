using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "0")]
    public class InvoicesController : BaseController
    {
        public InvoicesController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor)
        {
        }

        public async Task<IActionResult> Index(int? invoiceId)
        {
            var userid = await _user.GetUserId();
            var invoices =_context.Invoices.Include(i=>i.Enrollments).ThenInclude(e=>e.Payments).Where(i => i.Enrollments.Any(e => e.StudentId == userid));
            if (invoiceId.HasValue)
            {
                invoices = invoices.Where(i => i.Id == invoiceId);
            }
            return View(await invoices.ToListAsync());
        }
    }
}
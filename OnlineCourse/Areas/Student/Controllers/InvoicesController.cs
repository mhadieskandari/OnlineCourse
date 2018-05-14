using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Entity;

namespace OnlineCourse.Panel.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "0")]
    public class InvoicesController : BaseController
    {
        public InvoicesController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, PublicConfig config) : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor, config)
        {
        }

        public async Task<IActionResult> Index(int? invoiceId,int? enrollmentId)
        {
            try
            {
                var userid = await _user.GetUserId();
                var invoices = _context.Invoices.Include(i => i.Payments).ThenInclude(p=>p.Enrollment).ThenInclude(e=>e.Present).ThenInclude(p=>p.Section).ThenInclude(s=>s.Course).Where(i => i.Payments.Any(e => e.Enrollment.StudentId == userid));
                if (invoiceId.HasValue)
                {
                    invoices = invoices.Where(i => i.Id == invoiceId);
                }
                if (enrollmentId.HasValue)
                {
                    invoices = invoices.Where(i => i.Payments.Any(p=>p.EnrollmentId==enrollmentId));
                }
                return View(await invoices.ToListAsync());
            }
            catch (Exception e)
            {
                _historyService.LogError(e,HistoryErrorType.Middle);
                return View(null);
            }

        }
    }
}
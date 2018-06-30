using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OnlineCourse.Core;
using OnlineCourse.Core.Services;
using OnlineCourse.Panel.Utils.Extentions;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "10")]
    public class ClassRoomsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ClassRoomsController(ApplicationDbContext context, CurrentUser user, HistoryService historyService, IServiceProvider provider, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor, PublicConfig config)
            : base(context, user, historyService, provider, hostingEnvironment, httpContextAccessor, config)
        {

        }

        // GET: Admin/ClassRooms
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext = _context.ClassRooms.Include(c => c.Present);
                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // GET: Admin/ClassRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var classRoom = await _context.ClassRooms
                    .Include(c => c.Present)
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (classRoom == null)
                {
                    return NotFound();
                }

                return View(classRoom);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // GET: Admin/ClassRooms/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id");

                return View();
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }
        }

        // POST: Admin/ClassRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PresentId,StartedTime,EndedTime,Date,Description,Status,Source,ChangeTimePermit")] ClassRoom classRoom)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(classRoom);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id", classRoom.PresentId);
                return View(classRoom);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // GET: Admin/ClassRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var classRoom = await _context.ClassRooms.SingleOrDefaultAsync(m => m.Id == id);
                if (classRoom == null)
                {
                    return NotFound();
                }
                ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id", classRoom.PresentId);
                return View(classRoom);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // POST: Admin/ClassRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PresentId,StartedTime,EndedTime,Date,Description,Status,Source,ChangeTimePermit")] ClassRoom classRoom)
        {
            try
            {
                if (id != classRoom.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(classRoom);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClassRoomExists(classRoom.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["PresentId"] = new SelectList(_context.Presents, "Id", "Id", classRoom.PresentId);
                return View(classRoom);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // GET: Admin/ClassRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var classRoom = await _context.ClassRooms
                    .Include(c => c.Present)
                    .SingleOrDefaultAsync(m => m.Id == id);
                if (classRoom == null)
                {
                    return NotFound();
                }

                return View(classRoom);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        // POST: Admin/ClassRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var classRoom = await _context.ClassRooms.SingleOrDefaultAsync(m => m.Id == id);
                _context.ClassRooms.Remove(classRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                this.AddNotification("خطا در اتصال به پایگاه داده", NotificationType.Error);
                return RedirectToAction("ErrorPage","Home");
            }

        }

        private bool ClassRoomExists(int id)
        {
            try
            {
                return _context.ClassRooms.Any(e => e.Id == id);
            }
            catch (Exception e)
            {
                _historyService.LogError(e, HistoryErrorType.UI);
                return false;
            }
        }
    }
}

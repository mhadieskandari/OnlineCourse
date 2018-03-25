using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Entity;
using OnlineCourse.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using OnlineCourse.Panel.Utils.ViewModels.Areas.Admin;

namespace OnlineCourse.Panel.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "10")]
    public class TermsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TermsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Terms
        public async Task<IActionResult> Index(TermSearchViewModel term)
        {
            var list = _context.Terms.Where(t => t.Id > 0);
            if (term != null)
            {
                if (!string.IsNullOrEmpty(term.Title))
                {
                    list = list.Where(t => t.Title.Contains(term.Title));
                }
                if (!string.IsNullOrEmpty(term.Description))
                {
                    list = list.Where(t => t.Title.Contains(term.Description));
                }
                if (!string.IsNullOrEmpty(term.StartDate))
                {
                    var p = PersianDateTime.Core.PersianDateTime.Parse(term.StartDate);
                    list = list.Where(t => PersianDateTime.Core.PersianDateTime.Parse(t.StartDate) >= p);
                }
                if (!string.IsNullOrEmpty(term.EndDate))
                {
                    var p = PersianDateTime.Core.PersianDateTime.Parse(term.EndDate);
                    list = list.Where(t => PersianDateTime.Core.PersianDateTime.Parse(t.EndDate) <= p);
                }
                if (term.Year!=null)
                {
                    list = list.Where(t => t.Year==term.Year);
                }
                if (term.YearTerm != null)
                {
                    list = list.Where(t => t.YearTerm == term.YearTerm);
                }
                if (term.State != null)
                {
                    list = list.Where(t => t.State == term.State);
                }
                if (term.Type != null)
                {
                    list = list.Where(t => t.Type == term.Type);
                }
            }
            return View(await list.ToListAsync());
        }

        // GET: Admin/Terms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Terms
                .SingleOrDefaultAsync(m => m.Id == id);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // GET: Admin/Terms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Terms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,YearTerm,Description,StartDate,EndDate,Type,State")] Term term)
        {
            if (ModelState.IsValid)
            {
                _context.Add(term);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }

        // GET: Admin/Terms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Terms.SingleOrDefaultAsync(m => m.Id == id);
            if (term == null)
            {
                return NotFound();
            }
            return View(term);
        }

        // POST: Admin/Terms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,YearTerm,Description,StartDate,EndDate,Type,State")] Term term)
        {
            if (id != term.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(term);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermExists(term.Id))
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
            return View(term);
        }

        // GET: Admin/Terms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var term = await _context.Terms
                .SingleOrDefaultAsync(m => m.Id == id);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // POST: Admin/Terms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var term = await _context.Terms.SingleOrDefaultAsync(m => m.Id == id);
            _context.Terms.Remove(term);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TermExists(int id)
        {
            return _context.Terms.Any(e => e.Id == id);
        }
    }
}

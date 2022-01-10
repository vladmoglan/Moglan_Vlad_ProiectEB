using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moglan_Vlad_ProiectEB.Data;
using Moglan_Vlad_ProiectEB.Models;
using Microsoft.AspNetCore.Authorization;

namespace Moglan_Vlad_ProiectEB.Controllers
{
    public class ServicesController : Controller
    {
        private readonly CarServiceContext _context;

        public ServicesController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: Services
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var services = from b in _context.Services
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                services = services.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    services = services.OrderByDescending(b => b.Title);
                    break;
                case "Price":
                    services = services.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    services = services.OrderByDescending(b => b.Price);
                    break;
                default:
                    services = services.OrderBy(b => b.Title);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Service>.CreateAsync(services.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Services/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Appointments)
                .ThenInclude(e => e.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Price")] Service service)
        {
            try
            {
                if (ModelState.IsValid)
                {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            }

            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            return View(service);
        }

        // GET: Services/Edit/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Services.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Service>(
            studentToUpdate,
            "",
            s => s.Title, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Services/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (service == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ID == id);
        }
    }
}

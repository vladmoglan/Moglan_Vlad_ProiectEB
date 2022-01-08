using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moglan_Vlad_ProiectEB.Data;
using Moglan_Vlad_ProiectEB.Models;
using Moglan_Vlad_ProiectEB.Models.CarServiceViewModels;

namespace Moglan_Vlad_ProiectEB.Controllers
{
    public class LocationsController : Controller
    {
        private readonly CarServiceContext _context;

        public LocationsController(CarServiceContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index(int? id, int? serviceID)

        {
            var viewModel = new LocationIndexData();
            viewModel.Locations = await _context.Locations
            .Include(i => i.ProvidedService)
            .ThenInclude(i => i.Service)
            .ThenInclude(i => i.Appointments)
            .ThenInclude(i => i.Client)
            .AsNoTracking()
            .OrderBy(i => i.LocationName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["LocationID"] = id.Value;
                Location location = viewModel.Locations.Where(
                i => i.ID == id.Value).Single();
                viewModel.Services = location.ProvidedService.Select(s => s.Service);
            }
            if (serviceID != null)
            {
                ViewData["ServiceID"] = serviceID.Value;
                viewModel.Appointments = viewModel.Services.Where(
                x => x.ID == serviceID).Single().Appointments;
            }
            return View(viewModel);
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LocationName,Adress")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LocationName,Adress")] Location location)
        {
            if (id != location.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.ID))
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
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.ID == id);
        }
    }
}

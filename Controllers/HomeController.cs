using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moglan_Vlad_ProiectEB.Models;
using Microsoft.EntityFrameworkCore;
using Moglan_Vlad_ProiectEB.Data;
using Moglan_Vlad_ProiectEB.Models.CarServiceViewModels;

namespace Moglan_Vlad_ProiectEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarServiceContext _context;
        public HomeController(CarServiceContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Statistics()
        {
            IQueryable<AppointmentGroup> data =
            from appointment in _context.Appointments
            group appointment by appointment.AppointmentDate into dateGroup
            select new AppointmentGroup()
            {
                AppointmentDate = dateGroup.Key,
                ServiceCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using MechanicCompany.Data;
using MechanicCompany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MechanicCompany.Controllers
{
    public class MechanicsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public MechanicsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public ViewResult Index(string searchString)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var mechanics = _context.Mechanics.AsEnumerable().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                mechanics = mechanics.Where(s => s.FullName.Contains(searchString)).AsEnumerable().ToList();
            }
            return View(mechanics.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (id == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        public IActionResult Create()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,RoleInCompany")] Mechanic mechanic)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (ModelState.IsValid)
            {
                _context.Add(mechanic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mechanic);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (id == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null)
            {
                return NotFound();
            }
            return View(mechanic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,RoleInCompany")] Mechanic mechanic)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (id != mechanic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanicExists(mechanic.Id))
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
            return View(mechanic);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (id == null)
            {
                return NotFound();
            }

            var mechanic = await _context.Mechanics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanic == null)
            {
                return NotFound();
            }

            return View(mechanic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var mechanic = await _context.Mechanics.FindAsync(id);
            _context.Mechanics.Remove(mechanic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanicExists(int id)
        {
            return _context.Mechanics.Any(e => e.Id == id);
        }
    }
}

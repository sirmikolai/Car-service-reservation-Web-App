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
    public class RepairPartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public RepairPartsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            string id = "0";
            if (TempData["repairId"] != null)
                id = TempData["repairId"] as string;
            TempData.Keep();
            var repairRecordRepairStatus = "null";
            try
            {
                repairRecordRepairStatus = _context.RepairRecords
                 .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .Where(c => c.Id == int.Parse(id))
                .Select(c => c.StatusRepair)
                .FirstOrDefault().ToString();
            }
            catch (Exception)
            { }
            ViewData["StatusOfRepair"] = repairRecordRepairStatus;
            var applicationDbContext = _context.RepairParts.Include(r => r.RepairRecord).Where(w => w.RepairRecordId == int.Parse(id));
            return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            string id = "0";
            if (TempData["repairId"] != null)
                id = TempData["repairId"] as string;
            TempData.Keep();
            ViewData["RepairRecordId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RepairRecordId,PartName,PartCompany,PartCost,PartQuantity")] RepairPart repairPart)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            string id = "0";
            if (TempData["repairId"] != null)
                id = TempData["repairId"] as string;
            TempData.Keep();
            if (ModelState.IsValid)
            {
                _context.Add(repairPart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RepairRecordId"] = id;
            return View(repairPart);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (id == null)
            {
                return NotFound();
            }

            var repairPart = await _context.RepairParts
                .Include(r => r.RepairRecord)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairPart == null)
            {
                return NotFound();
            }

            return View(repairPart);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var repairPart = await _context.RepairParts.FindAsync(id);
            _context.RepairParts.Remove(repairPart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairPartExists(int id)
        {
            return _context.RepairParts.Any(e => e.Id == id);
        }
    }
}

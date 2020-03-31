using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MechanicCompany.Data;
using MechanicCompany.Models;
using Microsoft.Extensions.Configuration;

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

        // GET: RepairParts
        public async Task<IActionResult> Index()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            string id = "0";
            if (TempData["repairId"] != null)
                id = TempData["repairId"] as string;
            TempData.Keep();
            var applicationDbContext = _context.RepairParts.Include(r => r.RepairRecord).Where(w => w.RepairRecordId.Equals(int.Parse(id)));
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RepairParts/Create
        public IActionResult Create()
        {
            string id = "0";
            if (TempData["repairId"] != null)
                id = TempData["repairId"] as string;
            TempData.Keep();
            ViewData["RepairRecordId"] = id;
            return View();
        }

        // POST: RepairParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RepairRecordId,PartName,PartCompany,PartCost,PartQuantity")] RepairPart repairPart)
        {
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

        // GET: RepairParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

        // POST: RepairParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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

using MechanicCompany.Data;
using MechanicCompany.ListsHelper;
using MechanicCompany.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MechanicCompany.Controllers
{
    public class RepairRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public RepairRecordsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public ViewResult Index(string searchString)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var repairRecords = _context.RepairRecords.Include(r => r.Car).Include(r => r.Mechanic).AsEnumerable().Where(s => s.Car.ApplicationUserId.Equals(currentUserId)).ToList();
            if (currentUserEmail.Equals(companyMail))
            {
                repairRecords = _context.RepairRecords.Include(r => r.Car).Include(r => r.Mechanic).AsEnumerable().ToList();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                repairRecords = repairRecords.Where(s => s.Car.FullNameOfCar.Contains(searchString)
                                       || s.Mechanic.FullName.Contains(searchString)
                                       || s.Description.Contains(searchString)
                                       || s.StatusRepair.Contains(searchString)).AsEnumerable().ToList();
            }
            return View(repairRecords.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            TempData["repairId"] = id.ToString();
            if (id == null)
            {
                return NotFound();
            }

            var repairRecord = await _context.RepairRecords
                .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairRecord == null)
            {
                return NotFound();
            }

            var repairRecordLaborCost = _context.RepairRecords
                .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .Where(c => c.Id == id)
                .Select(c => c.LaborCost)
                .FirstOrDefault().ToString();

            var repairPartsCost = _context.RepairParts
                .Include(r => r.RepairRecord)
                .Where(m => m.RepairRecordId == id)
                .Sum(r => r.PartCost * r.PartQuantity);

            ViewData["CostOfParts"] = repairPartsCost.ToString();
            ViewData["AllRepairCosts"] = Double.Parse(repairPartsCost.ToString()) + Double.Parse(repairRecordLaborCost.ToString());
            return View(repairRecord);
        }

        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CarId"] = new SelectList(_context.Cars.Where(s => s.ApplicationUserId.Equals(currentUserId)), "Id", "FullNameOfCar");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            ViewData["StatusRepair"] = RepairRecordHelper.getRepairStatus();
            ViewData["StatusRepair2"] = "Pending";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,MechanicId,Description,StatusRepair,VisitDate,StartDate,EndDate,LaborCost")] RepairRecord repairRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repairRecord);
                await _context.SaveChangesAsync();
                var ToAddress = _configuration.GetSection("CompanyMail").Value;
                var userEmail = _context.Cars.Include(r => r.ApplicationUser).Where(r => r.Id.Equals(repairRecord.CarId)).Select(r => r.ApplicationUser.Email).FirstOrDefault().ToString();
                var carName = _context.Cars.Where(r => r.Id.Equals(repairRecord.CarId)).Select(r => r.FullNameOfCar).FirstOrDefault().ToString();
                var Body = "<div style='width: 70%; float: center'><center>" +
                    "<img src='https://i.imgur.com/JgvLADt.png' alt='Mechanic Company' height='99' width='300'/><hr>" +
                    "<p></p><p></p><p>" + "User: " + userEmail + " create repair for car: " + carName + " - " + repairRecord.Description.ToString() + "</p>" +
                    "<p>Please check details on our website. </p>" +
                    "<hr><p>Mechanic Company</p><p>Siewna 28, 42-201 Częstochowa</p><p>(48) 869 268 456</p><p>mikolaj.otreba@o2.pl</p>" +
                    "</center></div>";
                await _emailSender.SendEmailAsync(ToAddress, "Repair for car: " + carName + " - " + repairRecord.Description.ToString() + " was created.", Body);
                return RedirectToAction(nameof(Index));
            }
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CarId"] = new SelectList(_context.Cars.Where(s => s.ApplicationUserId.Equals(currentUserId)), "Id", "FullNameOfCar");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            ViewData["StatusRepair"] = RepairRecordHelper.getRepairStatus();
            return View(repairRecord);
        }

        // GET: RepairRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["repairId"] = id.ToString();
            if (id == null)
            {
                return NotFound();
            }

            var repairRecord = await _context.RepairRecords.FindAsync(id);
            if (repairRecord == null)
            {
                return NotFound();
            }
            var repairRecordRepairStatus = _context.RepairRecords
                .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .Where(c => c.Id == id)
                .Select(c => c.StatusRepair)
                .FirstOrDefault().ToString();

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "FullNameOfCar");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            ViewData["StatusRepair"] = RepairRecordHelper.getRepairStatus();
            ViewData["StatusOfRepair"] = repairRecordRepairStatus;
            return View(repairRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,MechanicId,Description,StatusRepair,VisitDate,StartDate,EndDate,LaborCost")] RepairRecord repairRecord)
        {
            if (id != repairRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repairRecord);
                    await _context.SaveChangesAsync();
                    var userEmail = _context.Cars.Include(r => r.ApplicationUser).Where(r => r.Id.Equals(repairRecord.CarId)).Select(r => r.ApplicationUser.Email).FirstOrDefault().ToString();
                    var carName = _context.Cars.Where(r => r.Id.Equals(repairRecord.CarId)).Select(r => r.FullNameOfCar).FirstOrDefault().ToString();
                    var Body = "<div style='width: 70%; float: center'><center>" +
                        "<img src='https://i.imgur.com/JgvLADt.png' alt='Mechanic Company' height='99' width='300'/><hr>" +
                        "<p></p><p></p><p>" + "Repair about your car: " + carName + " - " + repairRecord.Description.ToString() + " was update. </p>" +
                        "<p>Please check details on our website. </p>" +
                        "<hr><p>Mechanic Company</p><p>Siewna 28, 42-201 Częstochowa</p><p>(48) 869 268 456</p><p>mikolaj.otreba@o2.pl</p>" +
                        "</center></div>";
                    await _emailSender.SendEmailAsync(userEmail, "Repair for car: " + carName + " - " + repairRecord.Description.ToString() + " was update.", Body);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairRecordExists(repairRecord.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "FullNameOfCar");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            ViewData["StatusRepair"] = RepairRecordHelper.getRepairStatus();
            return View(repairRecord);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairRecord = await _context.RepairRecords
                .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repairRecord == null)
            {
                return NotFound();
            }

            return View(repairRecord);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairRecord = await _context.RepairRecords.FindAsync(id);
            _context.RepairRecords.Remove(repairRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairRecordExists(int id)
        {
            return _context.RepairRecords.Any(e => e.Id == id);
        }

    }
}

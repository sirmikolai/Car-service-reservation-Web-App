using MechanicCompany.Data;
using MechanicCompany.ListsHelper;
using MechanicCompany.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rotativa.AspNetCore;
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

        public ActionResult Index(string searchString)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var repairRecords = _context.RepairRecords.Include(r => r.Car).Include(r => r.Mechanic).AsEnumerable().Where(s => s.Car.ApplicationUserId.Equals(currentUserId)).ToList();
            try
            {
                if (currentUserEmail.Equals(companyMail))
                {
                    repairRecords = _context.RepairRecords.Include(r => r.Car).Include(r => r.Mechanic).AsEnumerable().ToList();
                }
            }
            catch (Exception)
            { }
            if (!String.IsNullOrEmpty(searchString))
            {
                repairRecords = repairRecords.Where(s => s.Car.FullNameOfCar.Contains(searchString)
                                       || s.Car.FullNameOfCar.ToLower().Contains(searchString)
                                       || s.Car.FullNameOfCar.ToUpper().Contains(searchString)
                                       || s.Mechanic.FullName.Contains(searchString)
                                       || s.Mechanic.FullName.ToLower().Contains(searchString)
                                       || s.Mechanic.FullName.ToUpper().Contains(searchString)
                                       || s.Description.Contains(searchString)
                                       || s.Description.ToLower().Contains(searchString)
                                       || s.Description.ToUpper().Contains(searchString)
                                       || s.StatusRepair.Contains(searchString)
                                       || s.StatusRepair.ToLower().Contains(searchString)
                                       || s.StatusRepair.ToUpper().Contains(searchString)).AsEnumerable().ToList();
            }
            return View(repairRecords.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
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

            var carId = _context.RepairRecords
                .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .Where(c => c.Id == id)
                .Select(c => c.CarId)
                .FirstOrDefault().ToString();

            var userNameForCar = _context.Cars
                .Include(c => c.ApplicationUser)
                .Where(c => c.Id == int.Parse(carId))
                .Select(d => d.ApplicationUser.Email)
                .FirstOrDefault();

            ViewData["UserNameForCar"] = userNameForCar;
            try
            {
                var repairRecordLaborCost = _context.RepairRecords
                    .Include(r => r.Car)
                    .Include(r => r.Mechanic)
                    .Where(c => c.Id == id)
                    .Select(c => c.LaborCost)
                    .FirstOrDefault();

                var repairPartsCost = _context.RepairParts
                    .Include(r => r.RepairRecord)
                    .Where(m => m.RepairRecordId == id)
                    .Sum(r => r.PartCost * r.PartQuantity);

                ViewData["CostOfParts"] = repairPartsCost.ToString()[0..^2];
                ViewData["AllRepairCosts"] = (Decimal.Parse(repairPartsCost.ToString()) + Decimal.Parse(repairRecordLaborCost.ToString())).ToString()[0..^2];
            }
            catch (Exception)
            {
                ViewData["CostOfParts"] = "0,00";
                ViewData["AllRepairCosts"] = "0,00";
            }
            return View(repairRecord);
        }

        public IActionResult Create()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
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
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (ModelState.IsValid)
            {
                _context.Add(repairRecord);
                await _context.SaveChangesAsync();
                var ToAddress = _configuration.GetSection("CompanyMail").Value;
                var userEmail = _context.Cars.Include(r => r.ApplicationUser).Where(r => r.Id.Equals(repairRecord.CarId)).Select(r => r.ApplicationUser.Email).FirstOrDefault().ToString();
                var carName = _context.Cars.Where(r => r.Id.Equals(repairRecord.CarId)).Select(r => r.FullNameOfCar).FirstOrDefault().ToString();

                var Body = "<div style='width: 70%; float: center'><center>" +
                    "<img src='https://i.imgur.com/JgvLADt.png' alt='Mechanic Company' height='99' width='300'/><hr>" +
                    "<p></p><p></p><p> User: " + userEmail + " create repair for car: " + carName + "</p><p>Description: " +
                    repairRecord.Description.ToString() + "</p><p>Please check details on our website.</p>" +
                    "<hr><div><strong>Mechanic Company</strong><br>Armii Krajowej 36, 42-202 Częstochowa<br>" +
                    "(48) 869 268 456<br>smtpserverforapp@gmail.com</div></center></div>";
                await _emailSender.SendEmailAsync(ToAddress, "Repair for car: " + carName + " was created.", Body);
                return RedirectToAction(nameof(Index));
            }
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["CarId"] = new SelectList(_context.Cars.Where(s => s.ApplicationUserId.Equals(currentUserId)), "Id", "FullNameOfCar");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            ViewData["StatusRepair"] = RepairRecordHelper.getRepairStatus();
            return View(repairRecord);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
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

            var repairRecordContext = _context.RepairRecords
                .Include(r => r.Car)
                .Include(r => r.Mechanic)
                .Where(c => c.Id == id);
            var repairRecordRepairStatus = repairRecordContext
                .Select(c => c.StatusRepair)
                .FirstOrDefault().ToString();

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "FullNameOfCar");
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "Id", "FullName");
            ViewData["StatusRepair"] = RepairRecordHelper.getRepairStatus();
            ViewData["StatusOfRepair"] = repairRecordRepairStatus;
            ViewData["LaborCost"] = repairRecordContext.Select(c => c.LaborCost).FirstOrDefault().ToString()[0..^3];
            return View(repairRecord);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,MechanicId,Description,StatusRepair,VisitDate,StartDate,EndDate,LaborCost")] RepairRecord repairRecord)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
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
                        "<p></p><p></p><p> Repair about your car: " + carName + " was update. </p><p>Description: " +
                        repairRecord.Description.ToString() + "</p><p>Please check details on our website.</p>" +
                        "<hr><div><strong>Mechanic Company</strong><br>Armii Krajowej 36, 42-202 Częstochowa<br>" +
                        "(48) 869 268 456<br>smtpserverforapp@gmail.com</div></center></div>";

                    await _emailSender.SendEmailAsync(userEmail, "Repair for car: " + carName + " was update.", Body);
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
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
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
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var repairRecord = await _context.RepairRecords.FindAsync(id);
            _context.RepairRecords.Remove(repairRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairRecordExists(int id)
        {
            return _context.RepairRecords.Any(e => e.Id == id);
        }

        public IActionResult Invoice()
        {
            string id = "0";
            if (TempData["repairId"] != null)
                id = TempData["repairId"] as string;
            TempData.Keep();
            DateTime date = DateTime.Today;
            string year = date.Year.ToString();
            var repairParts = _context.RepairParts.Include(w => w.RepairRecord).Where(w => w.RepairRecordId == int.Parse(id));
            try
            {
                #region DatabaseHelpers
                var repairRecord = _context.RepairRecords.Include(w => w.Car).Include(w => w.Mechanic).Where(r => r.Id == int.Parse(id));
                var carId = repairRecord.Select(r => r.CarId).FirstOrDefault();
                var car = _context.Cars.Include(w => w.ApplicationUser).Where(r => r.Id == carId);
                var userId = car.Select(r => r.ApplicationUserId).FirstOrDefault();
                var user = _context.ApplicationUsers.Where(w => w.Id == userId);
                var costOfParts = _context.RepairParts.Include(r => r.RepairRecord).Where(m => m.RepairRecordId == int.Parse(id)).Sum(r => r.PartCost * r.PartQuantity);
                var laborCost = repairRecord.Select(w => w.LaborCost).FirstOrDefault();
                #endregion
                #region ViewDataHelpers
                ViewData["Year"] = year;
                ViewData["DateOfIssue"] = repairRecord.Select(r => r.EndDate).FirstOrDefault().Value.Date.ToShortDateString();
                ViewData["LaborCost"] = laborCost;
                ViewData["AllRepairCost"] = (costOfParts + laborCost).ToString()[0..^2];
                ViewData["UserName"] = user.Select(r => r.FullName).FirstOrDefault();
                ViewData["UserAddress"] = user.Select(r => r.Address).FirstOrDefault();
                ViewData["UserZipCodeAndCity"] = user.Select(r => r.ZipCode + " " + r.City).FirstOrDefault();
                ViewData["UserPhone"] = user.Select(r => r.PhoneNumber).FirstOrDefault();
                ViewData["UserEmail"] = user.Select(r => r.Email).FirstOrDefault();
                ViewData["CarBrand"] = car.Select(r => r.CarBrand).FirstOrDefault();
                ViewData["CarModel"] = car.Select(r => r.CarModel).FirstOrDefault();
                ViewData["ProductionDate"] = car.Select(r => r.ProductionDate).FirstOrDefault().Date.ToShortDateString();
                ViewData["Engine"] = car.Select(r => r.EngineVolume + " " + r.EngineName + " - " + r.EnginePower + "hp - " + r.EngineTypeOfFuel).FirstOrDefault();
                ViewData["TypeOfBody"] = car.Select(r => r.CarTypeOfBody).FirstOrDefault();
                ViewData["RegistrationNumber"] = car.Select(r => r.RegistrationNumber).FirstOrDefault();
                #endregion
            }
            catch (Exception)
            { }

            return new ViewAsPdf(repairParts, ViewData) { FileName = String.Format("Proforma_Invoice_{0}.pdf", year + "/" + id) }; ;
        }

    }

}

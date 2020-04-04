using MechanicCompany.Data;
using MechanicCompany.ListsHelper;
using MechanicCompany.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MechanicCompany.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public CarsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public ViewResult Index(string searchString)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserEmail = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var cars = _context.Cars.Include(b => b.ApplicationUser).AsEnumerable().Where(b => b.ApplicationUserId.Equals(currentUserId)).ToList();
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            try
            {
                if (currentUserEmail.Equals(companyMail))
                {
                    cars = _context.Cars.Include(b => b.ApplicationUser).AsEnumerable().ToList();
                }
            }
            catch (Exception)
            { }
            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(s => s.FullNameOfCar.Contains(searchString)
                                       || s.FullNameOfCar.ToLower().Contains(searchString)
                                       || s.FullNameOfCar.ToUpper().Contains(searchString)
                                       || s.ApplicationUser.FullName.Contains(searchString)
                                       || s.ApplicationUser.FullName.ToLower().Contains(searchString)
                                       || s.ApplicationUser.FullName.ToUpper().Contains(searchString)).AsEnumerable().ToList();
            }
            return View(cars.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (id == null)
            {
                return NotFound();
            }

            var userNameForCar = _context.Cars
                .Include(c => c.ApplicationUser)
                .Where(c => c.Id == id)
                .Select(d => d.ApplicationUser.Email)
                .FirstOrDefault();
            ViewData["UserNameForCar"] = userNameForCar;

            var car = await _context.Cars
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult Create()
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            ViewBag.CurrentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["EngineTypeOfFuel"] = CarListHelper.getEngineTypeOfFuel();
            ViewData["CarTypeOfBody"] = CarListHelper.getCarTypeOfBody();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,CarBrand,CarModel,ProductionYear,EngineVolume,EngineName,EnginePower,EngineTypeOfFuel,CarTypeOfBody,RegistrationNumber")] Car car)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CurrentUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(car);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var userNameForCar = _context.Cars
                .Include(c => c.ApplicationUser)
                .Where(c => c.Id == id)
                .Select(d => d.ApplicationUser.Email)
                .FirstOrDefault();
            ViewData["UserNameForCar"] = userNameForCar;
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewBag.UserId = car.ApplicationUserId;
            ViewData["EngineTypeOfFuel"] = CarListHelper.getEngineTypeOfFuel();
            ViewData["CarTypeOfBody"] = CarListHelper.getCarTypeOfBody();
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,CarBrand,CarModel,ProductionYear,EngineVolume,EngineName,EnginePower,EngineTypeOfFuel,CarTypeOfBody,RegistrationNumber")] Car car)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var userNameForCar = _context.Cars
                .Include(c => c.ApplicationUser)
                .Where(c => c.Id == id)
                .Select(d => d.ApplicationUser.Email)
                .FirstOrDefault();
            ViewData["UserNameForCar"] = userNameForCar;
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            ViewBag.UserId = car.ApplicationUserId;
            return View(car);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var userNameForCar = _context.Cars
                .Include(c => c.ApplicationUser)
                .Where(c => c.Id == id)
                .Select(d => d.ApplicationUser.Email)
                .FirstOrDefault();
            ViewData["UserNameForCar"] = userNameForCar;
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyMail = _configuration.GetSection("CompanyMail").Value;
            ViewBag.CompanyMail = companyMail;
            var userNameForCar = _context.Cars
                .Include(c => c.ApplicationUser)
                .Where(c => c.Id == id)
                .Select(d => d.ApplicationUser.Email)
                .FirstOrDefault();
            ViewData["UserNameForCar"] = userNameForCar;
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}

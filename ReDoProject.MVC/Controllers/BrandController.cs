using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

namespace ReDoProject.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;
        private readonly Customer currentCustomer;
        public BrandController(
            ReDoMusicDbContext _dbContext)
        {
            _dbContext = _dbContext;

        }


        [HttpGet]
        public IActionResult Index()
        {
            var brands = _dbContext.Brands.Where(x=> x.IsDeleted == false).ToList();
            return View(brands);
        }

        public Customer GetCustomer()
        {

            if (currentCustomer is null)
            {
                var currentCustomerId = User.FindFirst(ClaimTypes.UserData)?.Value;
                return _dbContext.Customers
                    .FirstOrDefault(customerDB => customerDB.Id == Guid.Parse(currentCustomerId));
            }
            return currentCustomer;

        }


        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public IActionResult Add(string brandName, string brandDisplayingText, string brandAddress,string brandSMail,string brandSPhone)
        {
            
            try
            {
                var brand = new Brand()
                {
                    Name = brandName,
                    DisplayingText = brandDisplayingText,
                    Address = brandAddress,
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    SupportMail = brandSMail,
                    SupportPhone = brandSPhone,
                };

                _dbContext.Brands.Add(brand);

                _dbContext.Logs.Add(new MyLogger($"New Brand: {brand.Id}-{brand.Name} Added by {currentCustomer.Id}-{currentCustomer.Name}"));
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Marka başarıyla eklendi.";
            }
            catch
            {
                TempData["Error"] = "Please Fill All TextFields";
            }

            return View();

        }

        [Authorize(Policy = "AdminPolicy")]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            //_dbContext.Brands.Remove(brand);
            brand.IsDeleted = true;
            _dbContext.Logs.Add(new MyLogger($"Brand: {brand.Id}-{brand.Name} deleted by {currentCustomer.Id}-{currentCustomer.Name}"));

            _dbContext.SaveChanges();

            return RedirectToAction("index");
        }

     
    }
}

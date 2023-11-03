using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

namespace ReDoProject.MVC.Controllers
{
    public class BrandController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        public BrandController()
        {
            _dbContext = new();
        }


        [HttpGet]
        public IActionResult Index()
        {
            var brands = _dbContext.Brands.ToList();

           

            return View(brands);
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
            if (ModelState.IsValid)
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

                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Marka başarıyla eklendi.";
                return Redirect("Account/Register");
            }

            return View();

        }

        [Authorize(Policy = "AdminPolicy")]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();

            _dbContext.Brands.Remove(brand);

            _dbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}

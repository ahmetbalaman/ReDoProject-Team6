using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Persistence.Contexts;
using ReDoProject.Persistence.Contexts;
using ReDoProject.Domain;
using Microsoft.AspNetCore.Authorization;
using ReDoProject.Domain.Entities;
using System.Security.Claims;
using ReDoProject.Domain.Enums;

namespace ReDoProject.MVC.Controllers
{
   
    public class InstrumentController : Controller
    {
        private Customer currentCustomer;
        private readonly ReDoMusicDbContext _dbContext;


        public InstrumentController()
        { 
            _dbContext = new();
        }

        public Customer GetCustomer()
        {
            if(currentCustomer is null)
            {
                var currentCustomerId = User.FindFirst(ClaimTypes.UserData)?.Value;
                return _dbContext.Customers.Include(x => x.Basket).FirstOrDefault(x => x.Id == Guid.Parse(currentCustomerId));
            }
            return currentCustomer;
           
        }

        public IActionResult Index() //All Instruments will be shown
        {
            try
            {
                String id = User.FindFirst(ClaimTypes.UserData)?.Value;

                Console.WriteLine(id);
            //    Customer currentCustomer = _dbContext.Customers.FirstOrDefault(x => x.Id.ToString() == id);
                TempData["CurrentCustomerId"] = id; // currentCustomer'ı TempData'ye sakla

            }
            catch
            {
                Console.WriteLine("Current user invalid");
            }


            var products = _dbContext.Instruments.ToList();
           // _dbContext.Instruments.AddRange(ExampleData.GetInstruments());
           // _dbContext.SaveChanges();
            return View(products);
        }
        public IActionResult ListByInstrumentType(InstrumentType instrumentType)
        {
            var instruments = _dbContext.Instruments.Where(i => i.Type == instrumentType).ToList();
            return View(instruments);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult Add()
        {
            
            var brands = _dbContext.Brands.ToList();

            return View(brands);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public IActionResult Add(string name, string description, string brandId, string price, string barcode, string pictureUrl, List<Color> colors, InstrumentType type)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(brandId)).FirstOrDefault();
            decimal priceCorrect = 0;
            if(price != null)
            {
                priceCorrect = decimal.Parse(price);
            }


            Instrument instrument = new Instrument();


            instrument = new Instrument() {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Barcode = barcode,
                Price = priceCorrect,
                Color = colors,
                Brand = brand,
                CreatedOn = DateTime.UtcNow,
                PictureUrl = pictureUrl,

               Type = type,

            };

            _dbContext.Instruments.Add(instrument);

            _dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Enstruman başarıyla eklendi.";
            return RedirectToAction("add");
        }


        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Inspect(string id)
        {
            var instrument = _dbContext.Instruments.Where(x => x.Id == Guid.Parse(id)).FirstOrDefault();
            return View(instrument);
        }

        [HttpGet]
        
        public IActionResult AddBasket(String Id)
        {
            int count;

            Console.WriteLine(Id);
            try
            {

                Customer currentCustomer = GetCustomer();
                Instrument instrument = _dbContext.Instruments.FirstOrDefault(x => x.Id == Guid.Parse(Id));

                
                BasketItems ordered = new BasketItems()
                {
                    Instrument = instrument,
                    Quantity = 1,
                };
                ordered.CreatedByUserId = currentCustomer.Id.ToString();
                currentCustomer.Basket ??= new();
                currentCustomer.Basket.CreatedByUserId = currentCustomer.Id.ToString();
                if (TempData["basketCount"] != null){
                    
                    if (int.TryParse(TempData["basketCount"].ToString(),out count))
                    {
                        count++;
                        TempData["basketCount"] = count;
                    }
                }
                currentCustomer.Basket.BasketItems!.Add(ordered);
                _dbContext.SaveChanges();

            }
            catch
            {
                return Redirect("/Instrument");
            }
            

            return Redirect("/Account/Basket");

        }
        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                Instrument instrument = _dbContext.Instruments.FirstOrDefault(x => x.Id == Guid.Parse(id));
                _dbContext.Instruments.Remove(instrument);
                _dbContext.SaveChanges();
                return Redirect("/Instrument/Index");
            }
            catch
            {   
                return Redirect($"/Instrument/Inspect/{id}");
            }
            
        }

     

    }
}

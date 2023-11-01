using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Persistence.Contexts;
using ReDoProject.Persistence.Contexts;
using ReDoProject.Domain;
using Microsoft.AspNetCore.Authorization;
using ReDoProject.Domain.Entities;
using System.Security.Claims;

namespace ReDoProject.MVC.Controllers
{
   
    public class InstrumentController : Controller
    {
        private readonly Customer currentCustomer;
        private readonly ReDoMusicDbContext _dbContext;

        public InstrumentController()
        {
            _dbContext = new();

           
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


        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult Add()
        {
            var brands = _dbContext.Brands.ToList();

            return View(brands);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpPost]
        public IActionResult Add(string name, string description, string brandId, string price, string barcode, string pictureUrl)
        {
            var brand = _dbContext.Brands.Where(x => x.Id == Guid.Parse(brandId)).FirstOrDefault();
            decimal priceCorrect = 0;
            if(price != null)
            {
                priceCorrect = decimal.Parse(price);
            }


            Domain.Entities.Instrument instrument = new Domain.Entities.Instrument();


            instrument = new Domain.Entities.Instrument(){
                Id = Guid.NewGuid(),
               Name = "name",
                Description = "description",
                Barcode = "barcode",
                Price = priceCorrect,
                Color = new List<Domain.Enums.Color>(){Domain.Enums.Color.Black},
                Brand = brand,
                CreatedOn = DateTime.UtcNow,
                PictureUrl = "pictureUrl",

               Type = Domain.Enums.InstrumentType.AcousticPiano,

            };

            _dbContext.Instruments.Add(instrument);

            _dbContext.SaveChanges();

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
        public IActionResult AddBasket()
        {
            return View();

        }

        [HttpPost]
        
        public IActionResult AddBasket(String Id)
        {
            Console.WriteLine(Id);
            try
            {
                var currentCustomerId = User.FindFirst(ClaimTypes.UserData)?.Value;
                Instrument instrument = _dbContext.Instruments.FirstOrDefault(x => x.Id == Guid.Parse(Id));
                
                Customer currentCustomer = _dbContext.Customers.FirstOrDefault(x => x.Id== Guid.Parse(currentCustomerId));
                OrderedInstrument ordered = new OrderedInstrument()
                {
                    Instrument = instrument,
                    Quantity = 1
                };
                currentCustomer.Basket = new Basket();
                currentCustomer.Basket.OrderedInstruments = new List<OrderedInstrument>();
                currentCustomer.Basket.OrderedInstruments.Add(ordered);
                _dbContext.SaveChanges();

            }
            catch
            {
                return Redirect("/Instruments");
            }
            

            return Redirect("/Account");

        }
    }
}

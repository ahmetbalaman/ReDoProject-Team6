using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.MVC.Controllers
{
    public class AccountController : Controller
    {
        private Customer currentCustomer;
        private readonly ReDoMusicDbContext _dbContext;
       
        public AccountController()
        {
            _dbContext = new ReDoMusicDbContext();
        }

        public Customer GetCustomer()
        {
            if (currentCustomer is null)
            {
                var currentCustomerId = User.FindFirst(ClaimTypes.UserData)?.Value;
                return _dbContext.Customers.Include(x => x.Basket).ThenInclude(x => x.OrderedInstruments).ThenInclude(x => x.Instrument).FirstOrDefault(x => x.Id == Guid.Parse(currentCustomerId));
            }
            return currentCustomer;

        }



        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            Customer currentCustomer = GetCustomer();
            Console.WriteLine(currentCustomer.Orders);
            Console.WriteLine(currentCustomer.Orders.Count);
            Console.WriteLine(currentCustomer.Orders);
            return View(currentCustomer);
        }
        [HttpGet]
        public IActionResult Basket()
        {

            Customer currentCustomer = GetCustomer();
            var currentCustomerId = User.FindFirst(ClaimTypes.UserData)?.Value;
            Basket basket = _dbContext.Baskets.Include(x => x.OrderedInstruments).ThenInclude(x=> x.Instrument).FirstOrDefault(x=> x.Id == currentCustomer.Basket.Id);

            foreach(var model in basket.OrderedInstruments)
            {
                Console.WriteLine(model.Quantity);
            }

            return View(basket);
        }
        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult RemoveBasket(string id)
        {
            currentCustomer = GetCustomer();
            Basket basket = _dbContext.Baskets
                .Include(x => x.OrderedInstruments)
                    .FirstOrDefault(x => x.Id == currentCustomer.Basket.Id);


            var removeBasket = basket.OrderedInstruments.FirstOrDefault(x => x.Id == Guid.Parse(id));

            if (removeBasket != null)
            {
                basket.OrderedInstruments.Remove(removeBasket);
                _dbContext.SaveChanges();
            }
            //return View();
            return Redirect("/Account/Basket");
        }
        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public IActionResult OrderBasket(string id)
        {
           //BURAYI KESİNLİKLE İNCELE REFERANS SİLMESİ YAŞANIYOR BASKETLERDE.
            Customer currentCustomer = GetCustomer();
            Basket basket = _dbContext.Baskets
                .Include(x => x.OrderedInstruments)
                    .FirstOrDefault(x => x.Id == currentCustomer.Basket.Id);
            currentCustomer.Orders.Add(new Order(basket, false));


            //REFERANS SİLMESİ YAPMA KALSIN BURDA.
            basket.OrderedInstruments.Clear();
            basket = new();
            _dbContext.SaveChanges();





            return View();
            return Redirect("/Account/Index");
        }

        public IActionResult Login()
        {
            TempData["Error"] = "Login";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Email == email.ToLower() && x.Password == password) as Customer;
                if(customer == null)
                {
                    TempData["Error"] = "Acces Denied";
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.UserData,customer.Id.ToString()),
                      //  new Claim(ClaimTypes.MobilePhone, customer.PhoneNumber),
                      //  new Claim(ClaimTypes.Email, customer.Email),
                        new Claim(ClaimTypes.Name, customer.Name),

                        new Claim(ClaimTypes.Role, customer.Role.ToString()),
                    };
                    var userIdentitiy = new ClaimsIdentity(claims,"Login");
                    ClaimsPrincipal principal = new(userIdentitiy);
                    
                    await HttpContext.SignInAsync(principal);

                    
                    //giriş başarılı ise burdayız

                    // eğer admin ise başka sayfaya yönlendirebilirsin.
                    return Redirect("/Instrument");

                }
            }
            catch
            {
                TempData["Error"] = "Server Eror?";
            }

            

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            TempData["Error"] = "Register";
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer model)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    model.Email = model.Email.ToLower();
                    _dbContext.Customers.Add(model);
                    _dbContext.SaveChanges();
                    

                    return Redirect("/Account/Login");
               }
                catch
                {
                    TempData["Error"] = "Register Denied";
                }
            }


            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }


    }
}


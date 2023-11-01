using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ReDoMusicDbContext _context;

        public AccountController()
        {
            _context = new ReDoMusicDbContext();
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            String id = User.FindFirst(ClaimTypes.UserData)?.Value;
            Console.WriteLine($" {id} account controller");
            Customer currentCustomer = _context.Customers.FirstOrDefault(x => x.Id.ToString() == id);

            return View(currentCustomer);
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
                Customer customer = _context.Customers.FirstOrDefault(x => x.Email == email.ToLower() && x.Password == password) as Customer;
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
                TempData["Error"] = "What Did U DO??";
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
                    _context.Customers.Add(model);
                    _context.SaveChanges();
                    

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


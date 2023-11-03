using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.MVC.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;
        private Customer currentCustomer;
        public FavoritesController()
        {
            _dbContext = new();
        }

        public Customer GetCustomer()
        {
            if (currentCustomer is null)
            {
                var currentCustomerId = User.FindFirst(ClaimTypes.UserData)?.Value;
                return _dbContext.Customers.Include(x => x.FavBrands ).Include(x=> x.FavInstruments).FirstOrDefault(x => x.Id == Guid.Parse(currentCustomerId));
            }
            return currentCustomer;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            try
            {
                currentCustomer = GetCustomer();

                return View(currentCustomer);
            }
            catch
            {
                TempData["Error"] = "Please At Least add one product to favorite";
                return Redirect("/Instrument");
            }
            
        }
    }
}


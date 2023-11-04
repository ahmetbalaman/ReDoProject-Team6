using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.MVC.Controllers
{
    public class LogController : Controller
    {
        private readonly ReDoMusicDbContext _dbContext;

        public LogController()
        {
            _dbContext = new();
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet]
        public IActionResult Index()
        {
            List<MyLogger> logs = _dbContext.Logs.ToList();
            logs = logs.OrderBy(x => x.CreatedOn).Reverse().ToList();
            return View(logs);
        }
    }
}


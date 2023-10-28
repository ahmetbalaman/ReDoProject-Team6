using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.API.Controllers.Instrunments
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentController : ControllerBase
    {
        private readonly ReDoMusicDbContext _context;

        public InstrumentController()
        {
            _context = new ReDoMusicDbContext();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<List<Instrument>>> Get()
        {
            List<Instrument> _instruments = _context.Instruments.ToList();
            if (_instruments is null)
            {
                return NotFound("There is no Instruments");
            }

            return Ok();
        }


        [HttpGet("id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Instrument> GetById(Guid id)
        {
            Instrument _instrument = _context.Instruments.Where(x => x.Id == id).FirstOrDefault();
            return Ok(_instrument);
        }


    }
}


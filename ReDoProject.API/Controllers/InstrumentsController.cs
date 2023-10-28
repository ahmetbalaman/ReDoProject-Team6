using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.API.Validators;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.API.Controllers.Instrunments
{
    [Route("api/instruments")]
    [ApiController]
    public class InstrumentsController : ControllerBase
    {
        private readonly ReDoMusicDbContext _context;
        private readonly ValidationInstrument _validation;
        public InstrumentsController()
        {
            _context = new ReDoMusicDbContext();
            _validation = new ValidationInstrument(_context);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<List<Instrument>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Get()
        {
            List<Instrument> _instruments = _context.Instruments.ToList();
            if (_instruments is null)
            {
                return NotFound("There is no Instruments");
            }

            return Ok(_instruments);
        }


        [HttpGet("id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(Instrument))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetById(Guid id)
        {

          
            if(!_validation.validId(id)){
                return NotFound("there is no data with this Id");
            }


            Instrument _instrument = _context.Instruments.Where(x => x.Id == id).FirstOrDefault();
            return Ok(_instrument);
            
            
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Instrument))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Add([FromBody] Instrument model)
        {
            if(_validation.validId(model.Id)){
                return BadRequest($"This {model.Id.GetType()} is already in database.");
            }
            if (_validation.validModel(model))
            {
                return BadRequest("this is not a Instrument model");
            }

            _context.Instruments.Add(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = model.Id }, model);

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Update([FromBody] Instrument updatedInstrument)
        {
            if (!_validation.validId(updatedInstrument.Id))
            {
                return BadRequest($"there is no {updatedInstrument.Id} in database.");
            }
            if (_validation.validModel(updatedInstrument))
            {
                return BadRequest("this is not a Instrument model");
            }
            Instrument existingInstrument = _context.Instruments.FirstOrDefault(s=> s.Id == updatedInstrument.Id);
          
            existingInstrument.Name = updatedInstrument.Name;
            existingInstrument.Description = updatedInstrument.Description;
            existingInstrument.Brand = updatedInstrument.Brand;
            existingInstrument.Price = updatedInstrument.Price;
            existingInstrument.Color = updatedInstrument.Color;
            existingInstrument.Barcode = updatedInstrument.Barcode;
            existingInstrument.PictureUrl = updatedInstrument.PictureUrl;
            existingInstrument.Type = updatedInstrument.Type;
            _context.SaveChanges();
            return NoContent();

        }
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult Delete([FromBody] Guid id)
        {
            if (!_validation.validId(id))
            {
                return NotFound("There is no data with this id");
            }
            Instrument deletingInstrument = _context.Instruments.FirstOrDefault(s => s.Id == id);
            _context.Instruments.Remove(deletingInstrument);
            _context.SaveChanges();
            return NoContent();
        }



    }
}

/*
 * Person Kontroller 
 *     if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
 * 
 * 
 * */


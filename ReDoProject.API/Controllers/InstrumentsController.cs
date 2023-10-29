using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReDoProject.API.Validators;
using ReDoProject.Domain.Common;
using ReDoProject.Domain.Entities;
using ReDoProject.Domain.Enums;
using ReDoProject.Persistence.Contexts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReDoProject.API.Controllers.Instrunments
{
    [Route("api/instruments")]
    [ApiController]
    public class InstrumentsController : ControllerBase, IMyLogger
    {
        private readonly ErrorModel _error;
        private readonly ReDoMusicDbContext _context;
        private readonly ValidationInstrument _validation;
        public InstrumentsController()
        {
            _error = new ErrorModel()
            {
                ErrorResponseType = 0,
                ErrorMessage = new List<string>(),
            };
            
            _context = new ReDoMusicDbContext();
            _validation = new ValidationInstrument(_context);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<List<Instrument>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Get()
        {
            // Include, ThenInclude çalış.
            List<Instrument> _instruments = _context.Instruments.Where(x=> x.IsDeleted == false).Include(x => x.Brand).ToList();
            if (_instruments.Count == 0)
            {
                _error.ErrorResponseType = 404;
                _error.ErrorMessage.Add("There is no intruments");
                return NotFound(_error);
            }
            LogToDatabase($"called by $id");
            return Ok(_instruments);
        }


        [HttpGet("id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Instrument))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult GetById(Guid id)
        {


            if (!_validation.validId(id))
            {
                _error.ErrorMessage.Add("there is no data with this Id");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }


            Instrument _instrument = _context.Instruments.FirstOrDefault(x => x.Id == id);
            LogToDatabase($"called id by $id");
            return Ok(_instrument);


        }

        [HttpGet("type:Int")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Instrument))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult GetByType(int type)
        {
            if (type < 0)
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("U cant give me negative number");
                return BadRequest(_error);
            }

            List<Instrument> _instrument = _context.Instruments.Where(x => ((int)x.Type) == type).ToList();


           
            if(_instrument.Count == 0)
            {
                _error.ErrorMessage.Add("i dont have any instrument with this type");
                _error.ErrorResponseType = 404;
                // Making a error model?
                return NotFound(_error);
            }

            LogToDatabase($"called type by $id");
            return Ok(_instrument);


        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Instrument))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Add([FromBody] Instrument model)
        {
            if(_validation.validId(model.Id)){
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add($"This {model.Id.GetType()} is already in database.");
                return BadRequest(_error);
            }
            if (_validation.validModel(model))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("this is not a Instrument model");
                return BadRequest(_error);
            }

            _context.Instruments.Add(model);
            _context.SaveChanges();
            LogToDatabase($"added by $id");
            return CreatedAtRoute("GetById", new { id = model.Id }, model);

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Update([FromBody] Instrument updatedInstrument)
        {
            if (!_validation.validId(updatedInstrument.Id))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add($"there is no {updatedInstrument.Id} in database.");
                return BadRequest(_error);
            }
            if (_validation.validModel(updatedInstrument))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("this is not a Instrument model");
                return BadRequest(_error);
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
            LogToDatabase($"Updated by $id");
            return NoContent();

        }

        [HttpPatch("UpdatePrice")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult UpdatePrice([FromBody] Guid intrumentId,decimal price)
        {
           
            if (price < 0)
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("u cant define price negative.");
                return BadRequest(_error);

            }
            if (!_validation.validId(intrumentId))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add($"there is no {intrumentId} in database.");
                return BadRequest(_error);
            }
            
            Instrument existingInstrument = _context.Instruments.FirstOrDefault(s => s.Id == intrumentId);

            existingInstrument.Price = price;
            LogToDatabase($"updated price by $id");
            _context.SaveChanges();
            return NoContent();

        }
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Delete([FromBody] Guid id)
        {
          
            if (!_validation.validId(id))
            {
                _error.ErrorResponseType = 404;
                _error.ErrorMessage.Add("There is no data with this id");
                return NotFound(_error);
            }
            Instrument deletingInstrument = _context.Instruments.FirstOrDefault(s => s.Id == id);
            deletingInstrument.IsDeleted = true;
            //_context.Instruments.Remove(deletingInstrument);
            LogToDatabase($"deleted by $id");
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("DeleteForce")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult DeleteForce([FromBody] Guid id)
        {
            if (!_validation.validId(id))
            {
                _error.ErrorResponseType = 404;
                _error.ErrorMessage.Add("There is no data with this id");
                return NotFound(_error);
            }
            Instrument deletingInstrument = _context.Instruments.FirstOrDefault(s => s.Id == id);
          
            _context.Instruments.Remove(deletingInstrument);
            _context.SaveChanges();
            LogToDatabase("deleted forcefully by id");
            return NoContent();
        }

        public void LogToDatabase(string message)
        {
            _context.Logs.Add(new MyLogger() { logMessage = message });
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


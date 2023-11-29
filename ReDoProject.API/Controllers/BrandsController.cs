using Microsoft.AspNetCore.Mvc;
using ReDoProject.API.Validators;
using ReDoProject.Domain.Common;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly ErrorModel _error;
        private readonly ReDoMusicDbContext _context;
        private readonly ValidationBrand _validation;
        public BrandsController(
            )
        {
            _error = new ErrorModel()
            {
                ErrorResponseType = 0,
                ErrorMessage = new List<string>(),
            };

            _context = new ReDoMusicDbContext();
            _validation = new ValidationBrand(_context);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<List<Brand>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Get()
        {
            // Include, ThenInclude çalış.
            List<Brand> _brands = _context.Brands.Where(x=> x.IsDeleted == false).ToList();
            if (_brands.Count == 0)
            {
                _error.ErrorMessage.Add("There is no brands");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }
            //LogToDatabase("called by id");
            return Ok(_brands);
            
        }

        [HttpGet("id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Brand))]
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


            Brand _brand = _context.Brands.FirstOrDefault(x => x.Id == id);
            //LogToDatabase("called id by id");
            return Ok(_brand);


        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Brand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Add([FromBody] Brand model)
        {
            if (_validation.validId(model.Id))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add($"This {model.Id.GetType()} is already in database.");
                return BadRequest(_error);
            }
            if (_validation.validModel(model))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("this is not a Brand name");
                return BadRequest(_error);
            }

            _context.Brands.Add(model);
            _context.SaveChanges();
            //LogToDatabase("added by id");
            return CreatedAtRoute("GetById", new { id = model.Id }, model);

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Update([FromBody] Brand updatedBrand)
        {
            if (!_validation.validId(updatedBrand.Id))
            {
                _error.ErrorMessage.Add($"there is no {updatedBrand.Id} in database.");
                _error.ErrorResponseType = 400;
                return BadRequest(_error);
            }
            if (_validation.validModel(updatedBrand))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("this is not a Brand model");
                return BadRequest(_error);
            }
            Brand existingBrand = _context.Brands.FirstOrDefault(s => s.Id == updatedBrand.Id);

            existingBrand.Name = updatedBrand.Name;
            existingBrand.DisplayingText = updatedBrand.DisplayingText;
            existingBrand.Address = updatedBrand.Address;
            existingBrand.SupportMail = updatedBrand.SupportMail;
            existingBrand.SupportPhone = updatedBrand.SupportPhone;

            _context.SaveChanges();
            //LogToDatabase("updated by id");
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
                _error.ErrorMessage.Add("There is no data with this id");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }
            Brand deletingBrand = _context.Brands.FirstOrDefault(s => s.Id == id);
            deletingBrand.IsDeleted = true;
            //_context.Brands.Remove(deletingBrand);
            _context.SaveChanges();
            //LogToDatabase("deleted by id");
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
                _error.ErrorMessage.Add("There is no data with this id");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }
            Brand deletingBrand = _context.Brands.FirstOrDefault(s => s.Id == id);
           
            _context.Brands.Remove(deletingBrand);
            _context.SaveChanges();
            //LogToDatabase("deleted forcefully by id");
            return NoContent();
        }

       

       
    }
}


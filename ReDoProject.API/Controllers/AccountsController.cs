using Microsoft.AspNetCore.Mvc;
using ReDoProject.API.Validators;
using ReDoProject.Domain.Common;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReDoProject.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly ErrorModel _error;
        private readonly ReDoMusicDbContext _context;
        private readonly ValidationCustomer _validation;
        public AccountsController()
        {
            _error = new ErrorModel()
            {
                ErrorResponseType = 0,
                ErrorMessage = new List<string>(),
            };

            _context = new ReDoMusicDbContext();
            _validation = new ValidationCustomer(_context);
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<List<Customer>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Get()
        {
           
            List<Customer> _customers = _context.Customers.Where(x=> x.IsDeleted == false).Select(c => new Customer
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
            if (_customers.Count == 0)
            {
                _error.ErrorMessage.Add("There is no customers");
                _error.ErrorResponseType = 404;
                return NotFound(_error);
            }
            //LogToDatabase("called by id");
            return Ok(_customers);
            
        }

        [HttpGet("id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
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


            Customer _Customer = _context.Customers.Select(c => new Customer
            {
                Name = c.Name,
                Email = c.Email,
                Address = c.Address
            }).FirstOrDefault(x => x.Id == id);
            //LogToDatabase("called id by id");
            return Ok(_Customer);


        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Add([FromBody] Customer model)
        {
            if (_validation.validId(model.Id))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add($"This {model.Id.GetType()} is already in database.");
                return BadRequest(_error);
            }
            if (_validation.validEmail(model.Email))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("this is not a Customer name");
                return BadRequest(_error);
            }

            _context.Customers.Add(model);
            _context.SaveChanges();
            //LogToDatabase("added by id");
            return CreatedAtRoute("GetById", new { id = model.Id }, model);

        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorModel))]
        public IActionResult Update([FromBody] Customer
            updatedCustomer)
        {
            if (!_validation.validId(updatedCustomer.Id))
            {
                _error.ErrorMessage.Add($"there is no {updatedCustomer.Id} in database.");
                _error.ErrorResponseType = 400;
                return BadRequest(_error);
            }
            if (_validation.validEmail(updatedCustomer.Email))
            {
                _error.ErrorResponseType = 400;
                _error.ErrorMessage.Add("this is not a Customer model");
                return BadRequest(_error);
            }
            Customer existingCustomer = _context.Customers.FirstOrDefault(s => s.Id == updatedCustomer.Id);

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Surname = updatedCustomer.Surname;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;
            existingCustomer.PhoneNumber = updatedCustomer.PhoneNumber;


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
            Customer deletingCustomer = _context.Customers.FirstOrDefault(s => s.Id == id);
            deletingCustomer.IsDeleted = true;
            //_context.Customers.Remove(deletingCustomer);
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
            Customer deletingCustomer = _context.Customers.FirstOrDefault(s => s.Id == id);
           
            _context.Customers.Remove(deletingCustomer);
            _context.SaveChanges();
            //LogToDatabase("deleted forcefully by id");
            return NoContent();
        }

       

       
    }
}


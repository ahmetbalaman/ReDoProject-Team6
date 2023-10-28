using System;
using System.ComponentModel.DataAnnotations;
using ReDoProject.Persistence.Contexts;

namespace ReDoProject.API.Validators
{
	public class ValidationCustomer
	{
        private readonly ReDoMusicDbContext _context;

        public ValidationCustomer(ReDoMusicDbContext context)
        {
            _context = context;
        }

        public bool validEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false; 
            }
           
            bool emailExists = _context.Customers.Any(customer => customer.Email == email);
            return emailExists;
        }

        public bool validId(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false; 
            }
            
            bool idExists = _context.Customers.Any(customer => customer.Id == id);
            return idExists;

        }

        

    }
}


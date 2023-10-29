using System;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

namespace ReDoProject.API.Validators
{
	public class ValidationBrand
	{
        private readonly ReDoMusicDbContext _context;
        public ValidationBrand(ReDoMusicDbContext context)
		{
            _context = context;
        }
        public bool validModel(Object model)
        {
            if (model is Brand)
            {
                //if model is brand then no problem.
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool validId(Guid id)
        {
            if (id == Guid.Empty && id == null)
            {
                return false;

            }

            bool idExists = _context.Brands.Any(brand => brand.Id == id);
            return idExists;
        }
    }
}


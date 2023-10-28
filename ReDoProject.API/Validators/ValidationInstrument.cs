using System;
using ReDoProject.Domain.Entities;
using ReDoProject.Persistence.Contexts;

namespace ReDoProject.API.Validators
{
    public class ValidationInstrument
    {
        private readonly ReDoMusicDbContext _context;
        public ValidationInstrument(ReDoMusicDbContext context)
        {
            _context = context;
        }

        public bool validModel(Object model)
        {
            if (model is Instrument)
            {
                //if model is instrument then no problem.
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

            bool idExists = _context.Instruments.Any(instrument => instrument.Id == id);
            return idExists;
        }
    }

}


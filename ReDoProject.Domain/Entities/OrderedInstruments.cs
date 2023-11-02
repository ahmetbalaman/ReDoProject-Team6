using System;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{

        public class OrderedInstrument:EntityBase<Guid>
        {
            public Instrument Instrument { get; set; }
            public int Quantity { get; set; }

       
       
    }
    
}


using System;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
	public class Basket: EntityBase<Guid>
    {
        public List<OrderedInstrument>? OrderedInstruments { get; set; }
     
    }
}


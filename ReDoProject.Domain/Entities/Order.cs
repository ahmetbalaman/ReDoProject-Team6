using System;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
	
        public class Order:EntityBase<Guid>
        {
            public Basket OrderedBasket { get; set; }
            public bool IsDelivered { get; set; }
        }
    
}


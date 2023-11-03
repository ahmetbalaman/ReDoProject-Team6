using System;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
	public class Basket: EntityBase<Guid>
    {

        // implict operator override
        public List<BasketItems>? BasketItems { get; set; }
        public bool IsOrdered { get; set; }
        public Basket()
        {
            BasketItems = new();
        }

    }
}


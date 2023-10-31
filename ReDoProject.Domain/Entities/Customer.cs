using System;
namespace ReDoProject.Domain.Entities
{
	public class Customer:Person
	{
        public List<Instrument>? FavInstruments { get; set; }
        public List<Brand>? FavBrands { get; set; }
        public List<Basket>? Orders { get; set; }
        public List<OrderedInstrument>? Basket { get; set; }


    }
}


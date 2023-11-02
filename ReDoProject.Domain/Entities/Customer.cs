﻿using System;
namespace ReDoProject.Domain.Entities
{
	public class Customer:Person
	{

        public List<Instrument>? FavInstruments { get; set; }
        public List<Brand>? FavBrands { get; set; }
        public List<Order>? Orders { get; set; }
        public Basket? Basket { get; set; }

        public Customer()
        {
            Orders = new();
        }

    }
}


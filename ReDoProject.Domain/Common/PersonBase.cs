using System;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
	public class Person:EntityBase<Guid>
	{
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
    }
}


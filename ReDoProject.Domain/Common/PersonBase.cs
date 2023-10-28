using System;
using System.ComponentModel.DataAnnotations;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
	public class Person:EntityBase<Guid>
	{
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

      
    }
}


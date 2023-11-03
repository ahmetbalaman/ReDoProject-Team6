using System;
using System.ComponentModel.DataAnnotations;
using ReDoProject.Domain.Common;
using ReDoProject.Domain.Enums;

namespace ReDoProject.Domain.Entities
{
	public class Person:EntityBase<Guid>
	{
        [Required(ErrorMessage ="Who Are U???")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Joe?? Joe Who? Joe mama??")]
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "If I accept it, how will you enter without it??")]
        public string Password { get; set; }
        [Required(ErrorMessage = "I mean i need ur address for all orders :'(")]
        
        public string Address { get; set; }

        [Required(ErrorMessage = "hehehehe give it to me :) i swear i will not call u after night sss")]
        [Phone(ErrorMessage = "its not even a number wtf :/")]
        public string PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        public Role Role { get; set; } = Role.Customer;
    }
}


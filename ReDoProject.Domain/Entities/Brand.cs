using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReDoProject.Domain.Common;

namespace ReDoProject.Domain.Entities
{
    public class Brand : EntityBase<Guid>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayingText { get; set; }
        [Required]
        public string Address { get; set; }
        [EmailAddress]
        public string SupportMail { get; set; }
        [Phone]
        public string SupportPhone { get; set; }
    }
}

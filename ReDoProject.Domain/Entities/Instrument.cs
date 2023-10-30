using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReDoProject.Domain.Common;
using ReDoProject.Domain.Enums;

namespace ReDoProject.Domain.Entities
{
    public class Instrument : EntityBase<Guid>
    {
        [Required(ErrorMessage = "You have to give instrument name sir...")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "I mean why dont u give some detail?")]
        [StringLength(500)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Whose brand is this, quickly tell me??")]
        public Brand? Brand { get; set; }
        [Required(ErrorMessage = "Are you sure you want to sell this beauty for free??")]
        public decimal? Price { get; set; }
        public List<Color>? Color { get; set; }
        public string? Barcode { get; set; }

        [Url(ErrorMessage = "I can't sell what I haven't seen")]
        public string? PictureUrl { get; set; }
        [Required(ErrorMessage = "WHAT KIND INTRUMENT TYPE IT IS??, quickly tell me!")]
        public InstrumentType Type { get; set; }
    }
}


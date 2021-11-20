using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Models
{
    public class BoutiqueModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(30, ErrorMessage = "Error {0} the max lenght of the company name is{1} min lenght is {2}", MinimumLength = 2)]
        public string Country { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public string MobilePhone { get; set; }
    }
}

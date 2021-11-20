using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Data.Entities
{
    public class BoutiqueEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public string MobilePhone { get; set; }
        public virtual ICollection<ClothesEntity> Clothes { get; set; }
    }
}

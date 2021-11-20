using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Data.Entities
{
    public class ClothesEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal? Price { get; set; }
        public string Genre { get; set; }
        public int? Stock { get; set; }
        public int? Sell { get; set; } //Vendidos Sold
        [ForeignKey("BoutiqueIde")]
        public virtual BoutiqueEntity Boutique { get; set; }
    }
}

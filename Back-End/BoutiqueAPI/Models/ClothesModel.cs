using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoutiqueAPI.Models
{
    public class ClothesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
        public decimal? Price { get; set; }
        public string Genre { get; set; }
        public int Stock { get; set; }
        public int Sell { get; set; }
        public int BoutiqueIde { get; set; }
    }
}

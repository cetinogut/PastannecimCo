using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Models.DTO
{
    public class Cake
    {
        public string Size { get; set; }
        public string Flavour { get; set; }
        public string Frosting { get; set; }
        public string Topping { get; set; }
        public decimal Price { get; set; }
    }
}

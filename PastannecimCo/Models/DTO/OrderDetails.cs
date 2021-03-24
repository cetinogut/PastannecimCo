using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Models.DTO
{
    public class OrderDetails
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Cake Cake { get; set; }
    }
}

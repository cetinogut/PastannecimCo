using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual IList<CakeOrder> CakeOrders { get; set; }
    }
}

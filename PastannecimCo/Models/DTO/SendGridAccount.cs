using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Models.DTO
{
    public class SendGridAccount
    {
        public string ApiKey { get; set; }
        public string  EmailFromAddress { get; set; }
    }
}

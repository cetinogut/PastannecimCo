using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace PastannecimCo.Models.DTO
{
    public class Email
    {
        public EmailAddress From { get; set; }
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }

        public string FileName { get; set; } // for attachment invoce and iths path below
        public string FilePath { get; set; }

    }
}

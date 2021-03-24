using Microsoft.Extensions.Options;
using PastannecimCo.Models.DTO;
using PastannecimCo.Models.Entities;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Services.Implementations
{
    // install sendgrid nuget first. version 9.22 installed here..
    public class EmailService
    {
        private readonly SendGridAccount _account;
        public EmailService(IOptions<SendGridAccount> account) // startuptan bıraya getiriyoruz.
        {
            _account = account.Value ?? throw new ArgumentNullException(nameof(account));
        }

        public void SendEmail(CakeOrder cakeOrder)
        {
           
            var email = CreateEmail(cakeOrder); // aşağıdaki method da email oluşturacağız.
            var message = MailHelper.CreateSingleEmail(
                email.From, 
                email.To, 
                email.Subject,
                email.PlainTextContent, 
                email.HtmlContent);

            message.AddAttachment(); // AddAttachment methodu filename ve base64 codename istiyor. Base64 ü aşağıda encodeAttachment meyhodunda üreteceğiz.

        }

        private Email CreateEmail(CakeOrder cakeOrder)
        {

            var emailBody = $"Hello {cakeOrder.User.Name}, your cake order has been accepted. pls find your invoice here";

            var invoicePath = CreateInvoice();

            var email = new Email
            {
                To = new EmailAddress(cakeOrder.User.Email, cakeOrder.User.Name),
                From = new EmailAddress(_account.EmailFromAddress, "PastannecimCo"),
                Subject = $"Your invoice for invoice id {cakeOrder.Id}",
                HtmlContent = $"<strong>{emailBody}<strong>",
                PlainTextContent = emailBody,
                FileName = $"{cakeOrder.User.Name} {cakeOrder.Id}.pdf",
                FilePath = invoicePath


            };

            return email;

        }

        private string CreateInvoice()
        {
            return @"D:\23_MVC-Twilio-NETCore5\PastannecimCo\sample_invoice.pdf";
        }
    }
}

using Microsoft.Extensions.Options;
using PastannecimCo.Models.DTO;
using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Enums;
using PastannecimCo.Models.Helpers;
using PastannecimCo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace PastannecimCo.Services.Implementations
{
    public class MessagingService : IMessagingService

    // orderStatus = completed olunca whatsapp den kullanıcıya mesaj göndermek istiyoruz.
    {
        private readonly TwilioAccount _twilioAccount;

        public MessagingService(IOptions<TwilioAccount> twilioAccount)
        {
            _twilioAccount = twilioAccount.Value ?? throw new ArgumentNullException(nameof(twilioAccount)) ;
        }

        public async Task<ServiceResponse> SendMessage(CakeOrder cakeOrder)
        {
            TwilioClient.Init(_twilioAccount.AccountSID, _twilioAccount.AuthToken);

            var message = await MessageResource.CreateAsync(from: new PhoneNumber("whatsapp:+14155238886"), to: new PhoneNumber(cakeOrder.User.Number), body: $"Your cake order status code is {cakeOrder.OrderStatus.ToString()}..."); // burası whatsapp mesajını gönderecek.

            // aşağıda da mesajı gönderdikten sonra ServiceResponse ile mesajın gönderildiğini takip edebileceğiz.

            var response = new ServiceResponse
            {
                Message = "WhatsApp message was sent",
                ServiceResponseStatus = ServiceResponseStatus.Ok
            };

            return response; // var ı tanımlamadan yukarıda direk return new ServiceResponse {} diye de yapılabilirdi.

        }


    }
}

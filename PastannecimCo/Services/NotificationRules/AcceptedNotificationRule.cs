using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Enums;
using PastannecimCo.Models.Helpers;
using PastannecimCo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Services.NotificationRules
{
    public class AcceptedNotificationRule : IStatusNotificationRule
    {
        // Status Accepted olunca email göndermek istiyoruz bununu için burada email gönderme kuralını tanımlıyoruz. Bunu şartlı oalrak NotificationHandler içinden çağıracağız.

        private readonly IEmailService _emailService; //CakeOrderService içinden buraya taşıdık. Mesajlaşmanın CakeOrderService ile bir ilgilis yoktu. Mesajlaşma NotificationHandler tarafından yönetilecek, bu SOLIDe daha uygundur.

        public AcceptedNotificationRule(IEmailService emailService)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }
        public async Task<ServiceResponse> Notify (CakeOrder cakeOrder)
        {
            var serviceResponse = await _emailService.SendEmail(cakeOrder);

            return serviceResponse;
        }

         public bool StatusMatch (OrderStatus status)
        {
            return status == OrderStatus.Accepted;  // Accepted ise true döner
        }

    }
}

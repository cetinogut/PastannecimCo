using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Helpers;
using PastannecimCo.Services.Interfaces;
using PastannecimCo.Services.NotificationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Services.Implementations
{
    public class NotificationHandler :INotificationHandler
    {

        // orderStatus a göre ne şekilde notification gönderileceğine karar verecek bir handler yapıp daha önce CarOrderService içindeki mail ve message gönderme bölümlerini buraya aktaracağız. Böylece SOLID e de uymuş olacağız.

        private readonly IList<IStatusNotificationRule> _notificationRules; // DI için ctor a ekleyeceğiz

        public NotificationHandler(IEnumerable<IStatusNotificationRule> notificationRules)
        {
            _notificationRules = notificationRules.ToList();
        }


        public async Task<ServiceResponse> SendNotification (CakeOrder cakeOrder)
        {
            var serviceResponse = new ServiceResponse();
            // burada bir swittch ile ne tür notification göndereceğimize karar verebilirdik. Ama bunu yerine ruleset tanımlayıp kullandık.


            //yukarıda ctor da mevcut bütün notificationrule ları liste olarak aldık. Şimdi onların içinden bizim ckaeOrder'ın status una eşit olanı bulmamız lazım.

            IStatusNotificationRule rule = _notificationRules.FirstOrDefault(r => r.StatusMatch(cakeOrder.OrderStatus));

            if (rule != null)
            {
                serviceResponse = await rule.Notify(cakeOrder); // kuralı bulduysak burada kuralın notificationunun göndereceğiz.
            }



            return serviceResponse;

        }
    }
}

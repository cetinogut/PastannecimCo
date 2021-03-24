using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Enums;
using PastannecimCo.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Services.NotificationRules
{
    public interface IStatusNotificationRule
    {
        Task<ServiceResponse> Notify(CakeOrder cakeOrder);
        bool StatusMatch(OrderStatus status);
    }
}

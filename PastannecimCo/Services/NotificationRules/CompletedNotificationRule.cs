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
    public class CompletedNotificationRule : IStatusNotificationRule
    {
        private readonly IMessagingService _messagingService;

        public CompletedNotificationRule(IMessagingService messagingService)
        {
            _messagingService = messagingService ?? throw new ArgumentNullException(nameof(messagingService));
        }
        public async Task<ServiceResponse> Notify(CakeOrder cakeOrder)
        {
            return await  _messagingService.SendMessage(cakeOrder);
        }

        public bool StatusMatch(OrderStatus status)
        {
            return status == OrderStatus.Completed;
        }
    }
}

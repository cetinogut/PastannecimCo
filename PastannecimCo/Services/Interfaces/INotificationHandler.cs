using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Services.Interfaces
{
    public interface INotificationHandler
    {
        Task<ServiceResponse> SendNotification(CakeOrder cakeOrder);
    }
}

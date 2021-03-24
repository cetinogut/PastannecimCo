using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Helpers;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Services.Interfaces
{
    public interface IEmailService
    {
        Task<ServiceResponse> SendEmail(CakeOrder cakeOrder); // ilk önce SendEmail methodunu ürettik. Sonra bu interface i ekledik. Buradan da startup a gidip interface i ekledik. diğer servislerimizin altına ..(IUserRepository, ICakeOrderRepository, ICakeService, sonuncusu da IEmailService...). Buradan da gidip EmailService yi kullanacağımız CakeOrderService içinde constructor a bu IEmailService instancesini ekleyip DI yapacağız.
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastannecimCo.Data.Repositories;
using PastannecimCo.Models.DTO;
using PastannecimCo.Models.Entities;
using PastannecimCo.Models.Enums;
using PastannecimCo.Models.Helpers;
using PastannecimCo.Models.ViewModels;
using PastannecimCo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PastannecimCo.Services.Implementations
{
    public class CakeOrderService : ICakeOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICakeOrderRepository _cakeOrderRepository;
        private readonly INotificationHandler _notificationHandler;

        //private readonly IEmailService _emailService; //NotificationHandler e taşıdık, NotificiationRules içinde olacak.

        public CakeOrderService(IUserRepository userRepository,
            ICakeOrderRepository cakeOrderRepository, INotificationHandler notificationHandler)
        {
            _userRepository = userRepository?? throw new ArgumentNullException(nameof(userRepository));
            _cakeOrderRepository = cakeOrderRepository?? throw new ArgumentNullException(nameof(cakeOrderRepository));
            _notificationHandler = notificationHandler ?? throw new ArgumentNullException(nameof(notificationHandler));
            //_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService)); NotificiationRules içinde olacak.
        }

        public async Task<ServiceResponse<CakeOrderViewModel>> AddNewOrderAsync(OrderDetails orderDetails)
        {
            var serviceResponse = new ServiceResponse<CakeOrderViewModel>();

            var user = await _userRepository.GetUserByPhoneNumberAsync(orderDetails.Number);

            if (user == null)
            {
                user = new User
                {
                    Name = orderDetails.Name,
                    Number = orderDetails.Number,
                    Email = orderDetails.Email
                };

                user = await _userRepository.AddUserAsync(user);
            }
            
            var cakeOrder = new CakeOrder
            {
                Flavour = orderDetails.Cake.Flavour,
                Frosting = orderDetails.Cake.Frosting,
                Topping = orderDetails.Cake.Topping,
                Size = orderDetails.Cake.Size,
                Price = orderDetails.Cake.Price,
                OrderDate = DateTime.UtcNow,
                UserId =  user.Id,
                OrderStatus = OrderStatus.New
            };
  

            var cakeOrderEntity = await _cakeOrderRepository
                .AddCakeOrderAsync(cakeOrder)
                .ConfigureAwait(false);

            serviceResponse.Content = new CakeOrderViewModel(cakeOrderEntity, user);;
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Created;

            return serviceResponse;
        }

        public ServiceResponse<IList<CakeOrderViewModel>> GetAllCakeOrders()
        {
            var serviceResponse = new ServiceResponse<IList<CakeOrderViewModel>>();

            var ordersQuery = _cakeOrderRepository.GetAll(); // burada bütün orderları getiriyoruz

            var orders = ordersQuery.Include("User").ToList(); // burada da yukarıdaki orderlara iligli userları ekliyoruz

            if (orders.Count == 0)
            {
                serviceResponse.ServiceResponseStatus = ServiceResponseStatus.NotFound;
                return serviceResponse;
            }

            var cakeOrders = new List<CakeOrderViewModel>(); // automapper olsaydı onu burada kullanbilirdik, ama şimdi view model i iteration ile aşağıda dolduracağız.

            foreach (var order in orders)
            {
                var vm = new CakeOrderViewModel(order, order.User);
                cakeOrders.Add(vm);
            }

            serviceResponse.Content = cakeOrders;
            serviceResponse.Message = "all good";
            serviceResponse.ServiceResponseStatus = ServiceResponseStatus.Ok;

            return serviceResponse;

        }

        public async Task<ServiceResponse<CakeOrderViewModel>> GetOrderByIdAsync(int id)
        {
            var orderEntity = await _cakeOrderRepository
                .GetCakeOrderByIdAsync(id);
            
            var response = new ServiceResponse<CakeOrderViewModel>();

            if (orderEntity is null)
            {
                response.ServiceResponseStatus = ServiceResponseStatus.NotFound;
                response.Message = "no record available";
                return response; // this returns to CakeOrderController
            }
            
            response.Content = new CakeOrderViewModel(orderEntity, orderEntity.User); // burada repositoryden gelen order entiti ve içindeki user ı yukarıda oluşturduğumuz boş responde var ına altarıyoruz ve response.un contentinin dolduruyoruz. Aşağıda da response ı controller e gönderiyoruz

            response.ServiceResponseStatus = ServiceResponseStatus.Ok;
            
            return response; // this returns to CakeOrderController
        }

        public async Task<ServiceResponse<CakeOrderViewModel>> UpdateCakeOrderAsync(CakeOrderViewModel cakeOrder)
        {
            var response = new ServiceResponse<CakeOrderViewModel>();

            var entity = await _cakeOrderRepository.GetCakeOrderByIdAsync(cakeOrder.Id);

            entity.OrderStatus = cakeOrder.OrderStatus; // update de sadece status ü günceleyebiliyoruz şu an için. O yüzden o alanın viewden gelen değerini entity deki değere atıyoruz.// burada statu admin tarafından yönetim panelinden accepted e çevrilecek. ve sonrasında email göndereceğiz.

            //if (entity.OrderStatus == OrderStatus.Accepted) // bu bölümü NotificationHandler oluşturunca yorum haliine getirdik. Aşağıda notificationHandler ı çağırıp oradan dönen serviceResponse ile işlem iyapıyoruz artık. Mesaj gönderme ve email gönderme order ın statuüsne göre otomatik olacak.. accepted ise email, completed ise whatsapp mesajı gidecek otomatik olarak Twilio üzerinden.
            //{
            //    var emailResponse = await _emailService.SendEmail(entity);
            //}

            var cakeOrderEntity = await _cakeOrderRepository.UpdateAsync(entity);

            var serviceResponse = await _notificationHandler.SendNotification(cakeOrderEntity);

            response.Content = cakeOrder;
            response.ServiceResponseStatus = serviceResponse.ServiceResponseStatus;
            response.Message = serviceResponse.Message;

            return response;

        }

    }
}
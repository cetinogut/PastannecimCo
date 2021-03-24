using System.Collections.Generic;
using System.Threading.Tasks;
using PastannecimCo.Models.DTO;
using PastannecimCo.Models.Helpers;
using PastannecimCo.Models.ViewModels;

namespace PastannecimCo.Services.Interfaces
{
    public interface ICakeOrderService
    {
        Task<ServiceResponse<CakeOrderViewModel>> AddNewOrderAsync(OrderDetails orderDetails);
        ServiceResponse<IList<CakeOrderViewModel>> GetAllCakeOrders();

        Task<ServiceResponse<CakeOrderViewModel>> GetOrderByIdAsync(int id);
        Task<ServiceResponse<CakeOrderViewModel>> UpdateCakeOrderAsync(CakeOrderViewModel cakeOrder);
    }
}
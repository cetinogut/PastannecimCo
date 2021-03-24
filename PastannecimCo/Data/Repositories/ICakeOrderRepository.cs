using System.Linq;
using System.Threading.Tasks;
using PastannecimCo.Models.Entities;

namespace PastannecimCo.Data.Repositories
{
    public interface ICakeOrderRepository
    {
        IQueryable<CakeOrder> GetAll();
        Task<CakeOrder> AddCakeOrderAsync(CakeOrder cakeOrder);
        Task<CakeOrder> GetCakeOrderByIdAsync(int id);
        Task<CakeOrder> UpdateAsync(CakeOrder cakeOrder);
    }
}
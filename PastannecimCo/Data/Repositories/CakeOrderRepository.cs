using System.Linq;
using System.Threading.Tasks;
using PastannecimCo.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PastannecimCo.Data.Repositories
{
    public class CakeOrderRepository : ICakeOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public CakeOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IQueryable<CakeOrder> GetAll()
        {
            return _context.CakeOrders.AsQueryable();
        }
        
        public async Task<CakeOrder> AddCakeOrderAsync(CakeOrder cakeOrder)
        {
            var cakeEntity = await _context
                .CakeOrders.AddAsync(cakeOrder);

            await _context.SaveChangesAsync();

            return cakeEntity.Entity;
        }

        public async Task<CakeOrder> GetCakeOrderByIdAsync(int id)
        {
            return await _context.CakeOrders.Include("User")
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<CakeOrder> UpdateAsync(CakeOrder cakeOrder)
        {
            var entity =  _context.CakeOrders.Update(cakeOrder);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }
    }
}
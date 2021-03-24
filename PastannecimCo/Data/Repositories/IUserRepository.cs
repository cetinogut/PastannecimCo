using System.Threading.Tasks;
using PastannecimCo.Models.Entities;

namespace PastannecimCo.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByPhoneNumberAsync(string phoneNumber);
    }
}
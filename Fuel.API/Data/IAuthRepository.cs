using System.Threading.Tasks;
using Fuel.API.Models;

namespace Fuel.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<ClientProfile> Profile(ClientProfile profile, string fullName, string address1, string address2, string city, string state, string zipcode);
    }
}
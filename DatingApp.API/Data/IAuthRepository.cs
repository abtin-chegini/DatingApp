using System.Threading.Tasks;
using DatingApp.API.Model;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register (User user,string Password);
        Task<User> Login (string username ,string Password);
        Task<bool> UserExists (string username );
    }
}
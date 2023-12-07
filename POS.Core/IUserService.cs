using POS.Core.DTO;
using POS.DB;

namespace POS.Core
{
    public interface IUserService
    {
        Task<AuthenticatedUser> SignUp(User user);
        Task<AuthenticatedUser> SignIn(User user);
    }
}

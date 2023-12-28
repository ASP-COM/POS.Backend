using POS.Core.DTO;
using POS.DB.Models;

namespace POS.Core
{
    public interface IUserService
    {
        Task<AuthenticatedUser> SignUp(SignUpRequest request);
        Task<AuthenticatedUser> SignIn(SignInRequest request);
    }
}

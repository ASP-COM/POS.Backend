using POS.Core.CustomExceptions;
using POS.Core.DTO;
using POS.DB;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using POS.Core.Utilities;
using POS.DB.Models;

namespace POS.Core
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> SignIn(SignInRequest request)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == request.Name);

            if (dbUser == null
                || _passwordHasher.VerifyHashedPassword(dbUser.Password, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            return new AuthenticatedUser()
            {
                Id = dbUser.Id,
                Username = request.Name,
                Token = JwtGenerator.GenerateUserToken(request.Name)
            };
        }

        public async Task<AuthenticatedUser> SignUp(SignUpRequest request)
        {
            var checkUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Name.Equals(request.Name));

            // User Already exists, throwing an exception
            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            var newUser = new User
            {
                Name = request.Name,
                LastName = request.LastName,
                Password = request.Password,
                Email = request.Email,
                PhoneNo = request.PhoneNo,
            };

            newUser.Password = _passwordHasher.HashPassword(newUser.Password);

            // Fetch related entities based on associations
            if(request.BusinessId > 0)
            {
                newUser.Business = _context.Businesss.Find(request.BusinessId);
                if(newUser.Business != null)
                {
                    newUser.BusinessId = request.BusinessId;
                }
            }

            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Id = newUser.Id,
                Username = newUser.Name,
                Token = JwtGenerator.GenerateUserToken(newUser.Name)
            };
        }
    }
}

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

        public async Task<AuthenticatedUser> SignIn(User user)
        {
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == user.Name);

            if (dbUser == null
                || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            return new AuthenticatedUser()
            {
                Username = user.Name,
                Token = JwtGenerator.GenerateUserToken(user.Name)
            };
        }

        public async Task<AuthenticatedUser> SignUp(User user)
        {
            var checkUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Name.Equals(user.Name));

            // User Already exists, throwing an exception
            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException("Username already exists");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Name,
                Token = JwtGenerator.GenerateUserToken(user.Name)
            };
        }
    }
}

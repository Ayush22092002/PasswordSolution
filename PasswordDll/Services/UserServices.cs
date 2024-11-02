using Microsoft.AspNetCore.Identity;
using PasswordDll.Models; // Assuming the User model is in this namespace
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PasswordDll.Services
{
    public class UserServices
    {
        private readonly UserDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        // Constructor to inject UserDbContext and PasswordHasher
        public UserServices(UserDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // Method to register a new user with hashed password
        public async Task RegisterUser(User user)
        {
            // Hash the password before saving
            user.Password = _passwordHasher.HashPassword(user, user.Password);

            // Add user to the database and save changes
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Method to verify user login by comparing hashed password
        public async Task<bool> VerifyUser(string username, string password)
        {
            // Find the user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return false; // User not found
            }

            // Verify the hashed password
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}

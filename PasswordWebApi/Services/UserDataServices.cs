using PasswordDll.Models; // Reference to your User model and DbContext
using PasswordDll.Services; // Reference to the UserServices
using System.Threading.Tasks;

namespace PasswordWebApi.Services
{
    public class UserDataServices
    {
        private readonly UserServices _userServices;

        // Constructor to inject the UserServices class
        public UserDataServices(UserServices userServices)
        {
            _userServices = userServices;
        }

        // Method to register a new user
        public async Task RegisterUser(User user)
        {
            // Call the RegisterUser method from UserServices
            await _userServices.RegisterUser(user);
        }

        // Method to verify user login
        public async Task<bool> VerifyUser(string username, string password)
        {
            // Call the VerifyUser method from UserServices
            return await _userServices.VerifyUser(username, password);
        }
    }
}

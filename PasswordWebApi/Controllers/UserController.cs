using Microsoft.AspNetCore.Mvc;
using PasswordWebApi.Services;
using PasswordDll.Models; // Reference to your User model
using System.Threading.Tasks;

namespace PasswordWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDataServices _userDataServices;

        // Inject UserDataServices into the controller
        public UserController(UserDataServices userDataServices)
        {
            _userDataServices = userDataServices;
        }

        // API endpoint to register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                await _userDataServices.RegisterUser(user);
                return Ok("User registered successfully.");
            }
            return BadRequest("Invalid user data.");
        }

        // API endpoint to verify user login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            bool isValid = await _userDataServices.VerifyUser(loginModel.Username, loginModel.Password);
            if (isValid)
            {
                return Ok("Login successful.");
            }
            return Unauthorized("Invalid credentials.");
        }
    }

    // Simple model for login request
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

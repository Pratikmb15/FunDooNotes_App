using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using RepositoryLayer.Entity;
using System.Security.Claims;

namespace FunDooNotes.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthService _authService;

        public UserController(IUserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterModel Nuser)
        {
            User user = new User
            {
                FirstName = Nuser.FirstName,
                LastName = Nuser.LastName,
                Email = Nuser.Email,
                PasswordHash = Nuser.Password
            };

            _userService.RegisterUser(user);
            return Ok(new { Success = true, Message = "User registered successfully", Data = new { } });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserByEmail(request.Email); // Ensure this method returns User object
            if (user == null || !_userService.VerifyUser(request.Email, request.Password))
            {
                return Unauthorized(new { Success = false, Message = "Invalid credentials" });
            }

            var token = _authService.GenerateToken(user.Id, user.Email); // 🔹 Pass User ID to Token
            return Ok(new { Success = true, Message = "Login successful", Token = token });
        }
       
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { Success = false, Message = "Invalid token: User ID claim is missing." });
            }

            var user = _userService.GetUser(userId); 
            if (user == null)
            {
                return NotFound(new { Success = false, Message = "User not found" });
            }

            return Ok(new
            {
                Success = true,
                Message = "User profile data",
                Data = new { user.FirstName, user.LastName, user.Email }
            });
        }


    }
}

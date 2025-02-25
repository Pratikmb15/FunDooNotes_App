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
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterModel Nuser)
        {
            
            if (_userService.VerifyEmailExists(Nuser.Email)) {
                return BadRequest(new { Success = false, Message = "Email already exists" });
            }

            _userService.RegisterUser(Nuser);
            return Ok(new { Success = true, Message = "User registered successfully", Data = new { } });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.GetUserByEmail(request.Email); 
            if (user == null || !_userService.VerifyUser(request.Email, request.Password))
            {
                return Unauthorized(new { Success = false, Message = "Invalid credentials" });
            }

            var token = _authService.GenerateToken(user.Id, user.Email); 
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

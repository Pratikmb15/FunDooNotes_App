using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
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
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, IAuthService authService,IEmailService emailService)
        {
            _userService = userService;
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterModel Nuser)
        {

            if (_userService.VerifyEmailExists(Nuser.Email))
            {
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

        [HttpGet]
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
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPassModel model)
        {
            try
            {
                string resetLink = _userService.ForgetPassword(model.Email);
                await _emailService.SendEmailAsync(model.Email, resetLink);
                return Ok(new ResponseModel<string> { Success = true, Message = "Reset link sent to your email", Data = resetLink });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = e.Message });
            }
        }
        [HttpPost("set-newpassword/{token}")]
        public IActionResult SetNewPassword(string token, [FromBody] ResetPasswordModel model)
        {
            try
            {
                // 🔍 Validate Token and Extract Email
                if (!_authService.ValidateResetToken(token, out string email))
                {
                    return BadRequest(new { Success = false, Message = "Invalid or expired token." });
                }

                // 🔑 Reset Password
                bool isReset = _userService.ResetPassword(email, model);
                if (!isReset)
                {
                    return BadRequest(new { Success = false, Message = "Failed to reset password." });
                }

                return Ok(new { Success = true, Message = "Password reset successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An error occurred", Error = ex.Message });
            }
        }




        [HttpPost("reset-password")]
        [Authorize]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel resetPassModel)
        {
            if (resetPassModel.NewPassWord != resetPassModel.ConfirmPassWord)
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Passwords do not match" });
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new ResponseModel<string> { Success = false, Message = "Invalid token: Email claim is missing." });
            }

            if (_userService.ResetPassword(email, resetPassModel))
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Password reset successfully" });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Failed", Data = false });
            }
        }
       

    }
}


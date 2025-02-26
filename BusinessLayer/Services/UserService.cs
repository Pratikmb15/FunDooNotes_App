using BusinessLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository,IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public IEnumerable<User> GetAllUsers() => _userRepository.GetUsers();

        public User GetUser(int id) => _userRepository.GetUserById(id);

        public User GetUserByEmail(string email) => _userRepository.GetUserByEmail(email);

        public void RegisterUser(RegisterModel Nuser)
        {
            
            _userRepository.AddUser(Nuser);
        }

        public bool VerifyUser(string email, string password)
        {
            var user = _userRepository.GetUsers().FirstOrDefault(u => u.Email == email);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public void UpdateUser(User user) => _userRepository.UpdateUser(user);

        public void DeleteUser(int id) => _userRepository.DeleteUser(id);

        public bool VerifyEmailExists(string email) { 
            return _userRepository.CheckUserExists(email);
        }
        public string ForgetPassword(string email)
        {
            User user = _userRepository.GetUserByEmail(email);
            if (user == null)
                throw new Exception("User does not exist");

            int userId = user.Id;
            string newToken = _authService.GenerateToken(userId, email, true);
            return _userRepository.ForgetPassword(newToken, email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel model) {
            return _userRepository.ResetPassword(Email,model);
        }
    }
}

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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}

using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        User GetUserByEmail(String Email);
        void RegisterUser(RegisterModel Nuser);
        bool VerifyUser(string email, string password);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool VerifyEmailExists(string email);
    }
}

using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        User GetUserByEmail(String Email);
        void AddUser(RegisterModel Nuser);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool CheckUserExists(string email);
        string ForgetPassword(string newToken,string email);
        bool ResetPassword(string Email, ResetPasswordModel model);

    }
}

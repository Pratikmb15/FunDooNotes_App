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
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}

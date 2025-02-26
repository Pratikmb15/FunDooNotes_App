using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;

        }

        public IEnumerable<User> GetUsers() => _context.Users.ToList();

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetUserByEmail(String Email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == Email);
        }

        public void AddUser(RegisterModel Nuser)
        {
            User user = new User
            {
                FirstName = Nuser.FirstName,
                LastName = Nuser.LastName,
                Email = Nuser.Email,
                PasswordHash = Nuser.Password
            };
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PasswordHash = user.PasswordHash;
                _context.SaveChanges();
            }
        }


        public void DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public bool CheckUserExists(string email)
        {
            var alreadyExists = _context.Users.Any(u => u.Email == email);
            return alreadyExists;
        }
        public string ForgetPassword(string newToken, string email)
        {

            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                // Construct Reset Password Link
                string resetLink = $"http://localhost:5078/api/users/reset-password?token={newToken}";
                return resetLink;
            }
            else
            {
                throw new Exception("User does not exist");
            }
        }

        public bool ResetPassword(string Email, ResetPasswordModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);
            if (user != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.NewPassWord);
                _context.SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("User does not exist");
            }


        }
    }
}

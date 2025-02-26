using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(int userId, string email, bool isResetToken = false);
        bool ValidateResetToken(string token, out string email);
    }
}

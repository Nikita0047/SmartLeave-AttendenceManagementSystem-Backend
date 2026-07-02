using LeaveManagementSystem.Core.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Core.Dtos.Auth
{
    public class LoginResponseDto 
    {
        public string AccessToken { get; set; }= string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public UserSummeryDto Username { get; set; } = null!;
         public List<string> Permissions { get; set; } = new ();
    }
}

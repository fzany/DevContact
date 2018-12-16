using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevContact.Models
{
    public class AuthenticateResponse: UserResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}

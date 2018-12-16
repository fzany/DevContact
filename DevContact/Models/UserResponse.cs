using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevContact.Models
{
    public class UserResponse : GeneralResponse
    {
        public User Data { get; set; }
    }
   
    public class UserResponses : GeneralResponse
    {
        public List<User> Data { get; set; }
    }

   
}

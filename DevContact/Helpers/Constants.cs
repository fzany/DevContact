using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevContact.Helpers
{
    /// <summary>
    /// Holds contant messages
    /// </summary>
    public class Constants
    {
        public const string Error = "Error Occurred";
        public const string Email_Exists = "Email Exists";
        public const string Phone_Exists = "Phone Number Exists";
        public const string Success = "Success";
        public const string Non_Exist = "Account does not exist.";
        public const string Empty_List = "Empty List";
        public const string Provide_Guid = "Provide Guid";
        public const string Provide_Password = "Provide Password";
        public const string Provide_Email = "Provide Email";
        public const string Remove_Guid = "Guid field must be blank.";
        public const string Invalid_Email = "Invalid Email";

        public const string Invalid_Password = "Invalid Password";

    }

    /// <summary>
    /// Validate Email format
    /// </summary>
    public class Checks
    {
       public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}

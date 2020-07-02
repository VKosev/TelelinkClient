using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelelinkClient.Models
{
    public class ApplicationUser
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }
        public Owner Owner { get; set; }

        public bool IsValid()
        {
            if (UserName.Length <= 3 || UserName == null)
            {
                return false;
            }

            if (UserName.Any(Char.IsWhiteSpace))
            {
                return false;
            }
         
            if (Email.Length <= 3 || Email == null)
            {
                return false;
            }
            return true;
        }

        public string CheckError()
        {
            if (UserName.Length <= 3 || UserName == null)
            {
                return "UserName must be at least 4 characters.";
            }

            if (UserName.Any(Char.IsWhiteSpace))
            {
                return "Username can't have white spaces";
            }

            if (Owner.Name.Length <= 3 || Owner.Name == null)
            {
                return "Owner name must be at least 4 characters";
            }

            if (Email.Length <= 3 || Email == null)
            {
                return "Email is invalid";
            }
            return "User is valid";
        }
    }
}

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
        public String Email { get; set; }
        public Owner Owner { get; set; }
    }
}

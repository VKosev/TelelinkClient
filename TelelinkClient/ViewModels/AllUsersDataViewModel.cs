using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using TelelinkClient.Models;

namespace TelelinkClient.ViewModels
{
    public class AllUsersDataViewModel
    {
        public OwnerModel OwnerModels { get; set; }
        public String OwnerName { get; set; }
        public String ModelName { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
    }
}

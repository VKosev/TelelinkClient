using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelelinkClient.ViewModels
{
    public class UserDataViewModel
    {
        public string Username { get; set; }
        public string OwnerName { get; set; }
        public string Email { get; set; }
        public List<string> ModelsNames { get; set; }

        public UserDataViewModel()
        {
            ModelsNames = new List<String>();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelelinkClient.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ApplicationUser User { get; set; }
     
    }
}

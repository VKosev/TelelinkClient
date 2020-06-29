using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelelinkClient.ViewModels
{
    public class ModelAddsToOwnerCountViewModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int AddsToOwner { get; set; } // Shows count, how many times model is added to owner.
    }
}

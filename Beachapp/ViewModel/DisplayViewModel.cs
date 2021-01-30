using System.Collections.Generic;
using Beachapp.Data;
using Beachapp.Models;

namespace Beachapp.ViewModel
{
    public class DisplayViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Beach> Beaches { get; set; }
        
    }
}
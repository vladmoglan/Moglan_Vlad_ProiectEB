using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moglan_Vlad_ProiectEB.Models
{
    public class ProvidedService
    {
        public int LocationID { get; set; }
        public int ServiceID { get; set; }
        public Location Location { get; set; }
        public Service Service { get; set; }
    }
}

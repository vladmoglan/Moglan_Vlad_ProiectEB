using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moglan_Vlad_ProiectEB.Models.CarServiceViewModels
{
    public class LocationIndexData
    {
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}

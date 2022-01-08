using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moglan_Vlad_ProiectEB.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public int ClientID { get; set; }
        public int ServiceID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Client Client { get; set; }
        public Service Service { get; set; }
    }
}

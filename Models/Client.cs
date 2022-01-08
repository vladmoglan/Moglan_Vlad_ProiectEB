using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moglan_Vlad_ProiectEB.Models
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}

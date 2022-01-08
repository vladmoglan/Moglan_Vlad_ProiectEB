using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moglan_Vlad_ProiectEB.Models
{
    public class Service
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<ProvidedService> ProvidedService { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Moglan_Vlad_ProiectEB.Models.CarServiceViewModels
{
    public class AppointmentGroup
    {
        [DataType(DataType.Date)]
        public DateTime? AppointmentDate { get; set; }
        public int ServiceCount { get; set; }
    }
}

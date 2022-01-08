using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Moglan_Vlad_ProiectEB.Models
{
    public class Location
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Location Name")]
        [StringLength(50)]
        public string LocationName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<ProvidedService> ProvidedService { get; set; }

    }
}

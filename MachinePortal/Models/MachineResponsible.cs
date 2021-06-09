using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineResponsible
    {
        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }
        [Display(Name = "Responsible ID")]
        public int ResponsibleID { get; set; }

        [Display(Name = "Machine")]
        public Machine Machine { get; set; }
        [Display(Name = "Responsible")]
        public Responsible Responsible { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineResponsable
    {
        public int MachineID { get; set; }
        public int ResponsableID { get; set; }

        public Machine Machine { get; set; }
        public Responsible Responsable { get; set; }
    }
}

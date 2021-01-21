using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineResponsible
    {
        public int MachineID { get; set; }
        public int ResponsibleID { get; set; }

        public Machine Machine { get; set; }
        public Responsible Responsible { get; set; }
    }
}

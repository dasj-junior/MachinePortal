using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineDevice
    {
        public int MachineID { get; set; }
        public int DeviceID { get; set; }

        public Machine Machine { get; set; }
        public Device Device { get; set; }
    }
}

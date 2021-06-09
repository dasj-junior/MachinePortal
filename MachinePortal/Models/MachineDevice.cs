using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineDevice
    {
        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }
        [Display(Name = "Device ID")]
        public int DeviceID { get; set; }

        [Display(Name = "Machine")]
        public Machine Machine { get; set; }
        [Display(Name = "Device")]
        public Device Device { get; set; }
    }
}

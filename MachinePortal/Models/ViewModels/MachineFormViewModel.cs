using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class MachineFormViewModel
    {
        public Machine Machine { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Device> Devices { get; set; }
        public ICollection<Responsible> Responsibles { get; set; }
        public int AreaID { get; set; }
        public int SectorID { get; set; }
        public int LineID { get; set; }
    }
}

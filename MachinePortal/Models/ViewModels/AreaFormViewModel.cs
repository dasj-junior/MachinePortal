using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class AreaFormViewModel
    {
        public Area Area { get; set; }
        public ICollection<Sector> Sectors { get; set; }
    }
}

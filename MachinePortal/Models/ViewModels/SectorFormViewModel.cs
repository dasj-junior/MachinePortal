using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class SectorFormViewModel
    {
        public Sector Sector { get; set; }
        public ICollection<Line> Lines { get; set; }
        public ICollection<Area> Areas { get; set; }
    }
}

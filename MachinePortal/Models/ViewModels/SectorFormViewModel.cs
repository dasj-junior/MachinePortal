using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class SectorFormViewModel
    {
        public Sector Sector { get; set; }

        public SectorFormViewModel()
        {
        }

        public SectorFormViewModel(Sector sector)
        {
            Sector = sector;
        }
    }
}

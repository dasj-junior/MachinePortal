using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class SectorFormViewModel
    {
        [Display(Name = "Sector")]
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

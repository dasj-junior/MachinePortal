using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models.ViewModels
{
    public class MachineFormViewModel
    {
        [Display(Name = "Machine")]
        public Machine Machine { get; set; }
        [Display(Name = "Devices")]
        public ICollection<Device> Devices { get; set; }
        [Display(Name = "Responsibles")]
        public ICollection<Responsible> Responsibles { get; set; }
        [Display(Name = "Selected Devices")]
        public int[] SelectedDevices { get; set; }
        [Display(Name = "Selected Responsibles")]
        public int[] SelectedResponsibles { get; set; }
        [Display(Name = "Area")]
        public int AreaID { get; set; }
        [Display(Name = "Sector")]
        public int SectorID { get; set; }
        [Display(Name = "Line")]
        public int LineID { get; set; }
    }
}

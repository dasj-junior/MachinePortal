using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Machine
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        [Display(Name = "Asset Number")]

        public int AssetNumber { get; set; }
        [Display(Name = "MES Name")]
        public string MES_Name { get; set; }
        [Display(Name = "SAP Name")]
        public string SAP_Name { get; set; }
        public string WorkCenter { get; set; }
        [Display(Name = "Cost Center")]
        public int CostCenter { get; set; }
        [Display(Name = "Server Path")]
        public string ServerPath { get; set; }
        [Display(Name = "Last Preventive Maintenance")]
        public DateTime LastPreventiveMaintenance { get; set; }
        [Display(Name = "Start In Production Date")]
        public DateTime StartDate { get; set; }

        public int AreaID { get; set; }
        public Area Area { get; set; }

        public int SectorID { get; set; }
        public Sector Sector { get; set; }

        public int LineID { get; set; }
        public Line Line { get; set; }

        public ICollection<MachineDocument> MachineDocuments { get; set; }
        public ICollection<MachineComment> MachineComments { get; set; }
        public ICollection<MachineImage> MachineImages { get; set; }
        public ICollection<MachineVideo> MachineVideos { get; set; }

        public ICollection<MachineResponsible> MachineResponsibles { get; set; }
        public ICollection<MachineDevice> MachineDevices { get; set; }

        public ICollection<Password> Passwords { get; set; }

        public void AddDocument(MachineDocument machineDocument)
        {
            MachineDocuments.Add(machineDocument);
        }

        public void AddImage(MachineImage machineImage)
        {
            MachineImages.Add(machineImage);
        }

        public void AddVideo(MachineVideo machineVideo)
        {
            MachineVideos.Add(machineVideo);
        }

    }
}

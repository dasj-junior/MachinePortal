using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Machine
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Display(Name = "Photo")]
        public string ImagePath { get; set; }
        
        [Display(Name = "Asset Number")]
        public string AssetNumber { get; set; }
        
        [Display(Name = "MES Name")]
        public string MES_Name { get; set; }
        
        [Display(Name = "SAP Name")]
        public string SAP_Name { get; set; }

        [Display(Name = "Work Center")]
        public string WorkCenter { get; set; }
        
        [Display(Name = "Cost Center")]
        public string CostCenter { get; set; }
        
        [Display(Name = "Server Path")]
        public string ServerPath { get; set; }
        
        [Display(Name = "Last Preventive Maintenance Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime LastPreventiveMaintenance { get; set; }
        
        [Display(Name = "Manufactured Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Area ID")]
        public int AreaID { get; set; }
        [Display(Name = "Area")]
        public Area Area { get; set; }

        [Display(Name = "Sector ID")]
        public int SectorID { get; set; }
        [Display(Name = "Sector")]
        public Sector Sector { get; set; }

        [Display(Name = "Line ID")]
        public int LineID { get; set; } 
        [Display(Name = "Line")]
        public Line Line { get; set; }

        [Display(Name = "Machine Documents")]
        public ICollection<MachineDocument> MachineDocuments { get; set; }
        [Display(Name = "Machine Comments")]
        public ICollection<MachineComment> MachineComments { get; set; }
        [Display(Name = "Machine Images")]
        public ICollection<MachineImage> MachineImages { get; set; }
        [Display(Name = "Machine Videos")]
        public ICollection<MachineVideo> MachineVideos { get; set; }
        [Display(Name = "Machine Responsibles")]
        public ICollection<MachineResponsible> MachineResponsibles { get; set; }
        [Display(Name = "Machine Devices")]
        public ICollection<MachineDevice> MachineDevices { get; set; }
        [Display(Name = "Passwords ")]
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

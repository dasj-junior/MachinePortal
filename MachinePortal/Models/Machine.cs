using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Machine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int AssetNumber { get; set; }
        public string MES_Name { get; set; }
        public string SAP_Name { get; set; }
        public string WorkCenter { get; set; }
        public int CostCenter { get; set; }
        public string ServerPath { get; set; }

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

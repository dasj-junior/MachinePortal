using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class DeviceDocument
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        [Display(Name = "Extension")]
        public string Extension { get; set; }

        [Display(Name = "Device ID")]
        public int DeviceID { get; set; }
        [Display(Name = "Device")]
        public Device Device { get; set; } 
        
        public DeviceDocument()
        {
        }

        public DeviceDocument(int Id, string name, string path, string extension, Device device)
        {
            ID = Id;
            Name = name;
            Path = path;
            Extension = extension;
            Device = device;
        }
    }
}

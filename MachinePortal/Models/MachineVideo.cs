using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineVideo
    {
        public int ID { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public string Location { get; set; } //File ServerPath WebLink 

        public int MachineID { get; set; }
        public Machine Machine { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public MachineVideo()
        {
        }

        public MachineVideo(int Id, string name, string category, string filename, string path, string extension, string type)
        {
            ID = Id;
            Name = name;
            //Category = category;
            FileName = filename;
            Path = path;
            Extension = extension;
            Type = type;
        }
    }
}

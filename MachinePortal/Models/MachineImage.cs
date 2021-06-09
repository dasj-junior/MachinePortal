using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineImage
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Display(Name = "Path")]
        public string Path { get; set; }

        [Display(Name = "Extension")]
        public string Extension { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; } //File ServerPath WebLink 

        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }
        [Display(Name = "Machine")]
        public Machine Machine { get; set; }

        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        [Display(Name = "Category")]
        public Category Category { get; set; }

        public MachineImage()
        {
        }

        public MachineImage(int Id, string name, string category, string filename, string path, string extension, string type)
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

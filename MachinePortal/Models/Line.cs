using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Line
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Photo")]
        public string ImagePath { get; set; }

        [Display(Name = "Sector ID")]
        public int SectorID { get; set; }
        [Display(Name = "Sector")]
        public Sector Sector { get; set; }

        public Line()
        {
        }

        public Line(int Id, string name, string imagePath)
        {
            ID = Id;
            Name = name;
            ImagePath = imagePath;
        }
    }
}

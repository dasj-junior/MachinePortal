using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Line
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public int SectorID { get; set; }
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Area
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Photo")]
        public string ImagePath { get; set; }
        public ICollection<Sector> Sectors { get; set; }

        public Area()
        {
        }

        public Area(int Id, string name)
        {
            ID = Id;
            Name = name;
            ImagePath = ImagePath;
        }
    }
}

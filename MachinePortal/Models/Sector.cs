using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Sector
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Photo")]
        public string ImagePath { get; set; }

        [Display(Name = "Lines")]
        public ICollection<Line> Lines { get; set; }

        [Display(Name = "Area ID")]
        public int AreaID { get; set; }
        [Display(Name = "Area")]
        public Area Area { get; set; }

        public Sector()
        {
        }

        public Sector(int Id, string name, string imagePath)
        {
            ID = Id;
            Name = name;
            ImagePath = imagePath;
        }
    }
}

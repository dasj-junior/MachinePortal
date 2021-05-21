using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Sector
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public ICollection<Line> Lines { get; set; }

        public int AreaID { get; set; }
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

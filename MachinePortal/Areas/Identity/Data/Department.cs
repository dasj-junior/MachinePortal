using MachinePortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinePortal.Areas.Identity.Data
{
    public class Department
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<Responsible> Responsibles { get; set; }

        public Department()
        {
        }

        public Department(int Id, string name)
        {
            ID = Id;
            Name = name;
        }
    }
}

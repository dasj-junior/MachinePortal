using MachinePortal.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinePortal.Areas.Identity.Data
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; }

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

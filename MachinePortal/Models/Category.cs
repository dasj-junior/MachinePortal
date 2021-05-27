using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Category()
        {
        }

        public Category(int Id, string name)
        {
            ID = Id;
            Name = name;
        }
    }
}

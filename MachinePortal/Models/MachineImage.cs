using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineImage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }

        public MachineImage()
        {
        }

        public MachineImage(int Id, string name, string path, string extension)
        {
            ID = Id;
            Name = name;
            Path = path;
            Extension = extension;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineDocument
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }

        public MachineDocument()
        {
        }

        public MachineDocument(int Id, string name, string path, string extension, string type)
        {
            ID = Id;
            Name = name;
            Path = path;
            Extension = extension;
            Type = type;
        }
    }
}

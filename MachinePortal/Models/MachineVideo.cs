using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineVideo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }

        public MachineVideo()
        {
        }

        public MachineVideo(int Id, string name, string path, string extension)
        {
            ID = Id;
            Name = name;
            Path = path;
            Extension = extension;
        }
    }
}

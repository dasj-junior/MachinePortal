using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class DeviceDocument
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }

        public int DeviceID { get; set; }
        public Device Device { get; set; }

        public DeviceDocument()
        {
        }

        public DeviceDocument(int Id, string name, string path, string extension, Device device)
        {
            ID = Id;
            Name = name;
            Path = path;
            Extension = extension;
            Device = device;
        }
    }
}

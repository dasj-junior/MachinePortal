using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Asset
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public double InitialValue { get; set; }
        public string ImageURL { get; set; }

        public int MachineID { get; set; }
        public Machine Machine { get; set; }

        public Asset()
        {
        }

        public Asset(int Id, string name, int number, string description, double initialValue, string imageURL)
        {
            ID = Id;
            Name = name;
            Number = number;
            Description = description;
            InitialValue = initialValue;
            ImageURL = imageURL;
        }
    }
}

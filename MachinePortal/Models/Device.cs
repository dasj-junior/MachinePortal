using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Device
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Supplier { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Document> Documents { get; set; }

        public Device()
        {
        }

        public Device(int Id, string name, string brand, string model, string partNumber, string description, double price, string supplier, string imagePath)
        {
            ID = Id;
            Name = name;
            Brand = brand;
            Model = model;
            PartNumber = partNumber;
            Description = description;
            Price = price;
            Supplier = supplier;
            ImagePath = imagePath;
        }

        public void AddDocument(Document doc)
        {
            Documents.Add(doc);
        }

        public void RemoveDocument(Document doc)
        {
            Documents.Remove(doc);
        }
    }

}

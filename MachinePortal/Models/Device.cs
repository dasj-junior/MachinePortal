using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Device
    {
        public int ID { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Brand { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Model { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string PartNumber { get; set; }
        [StringLength(200, MinimumLength = 3)]
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string Supplier { get; set; }
        [StringLength(100, MinimumLength = 3)]
        public string StockLocation { get; set; }
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public ICollection<DeviceDocument> Documents { get; set; }

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

        public void AddDocument(DeviceDocument doc)
        {
            Documents.Add(doc);
        }

        public void RemoveDocument(DeviceDocument doc)
        {
            Documents.Remove(doc);
        }
    }

}

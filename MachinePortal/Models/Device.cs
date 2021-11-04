using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Device
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Brand")]
        public string Brand { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Model")]
        public string Model { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Part Number")]
        public string PartNumber { get; set; }
        
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Supplier")]
        public string Supplier { get; set; }
        
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "StockLocation")]
        public string StockLocation { get; set; }
        
        [Display(Name = "Photo")]
        public string ImagePath { get; set; }

        public ICollection<DeviceDocument> Documents { get; set; }

        public Device()
        {
        }

        public Device(int Id, string name, string brand, string model, string partNumber, string description, float price, string supplier, string imagePath)
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

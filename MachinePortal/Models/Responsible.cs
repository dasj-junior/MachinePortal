using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Responsible
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Department { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        public Responsible()
        {
        }

        public Responsible(int Id, string firstName, string lastName, string department, string phoneNumber, string mobile, string email, string photoPath)
        {
            ID = Id;
            FirstName = firstName;
            LastName = lastName;
            FullName = firstName + " " + LastName;
            Department = department;
            PhoneNumber = phoneNumber;
            Mobile = mobile;
            Email = email;
            PhotoPath = photoPath;
        }
    }
}

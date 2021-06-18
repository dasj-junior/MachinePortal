using System.ComponentModel.DataAnnotations;
using MachinePortal.Areas.Identity.Data;

namespace MachinePortal.Models
{
    public class Responsible
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        
        [Display(Name = "Job Role")]
        public string JobRole { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        [Display(Name = "Department")]
        public Department Department { get; set; }
        [Display(Name = "Department ID")]
        public int DepartmentID { get; set; }
        

        public Responsible()
        {
        }

        public Responsible(int Id, string firstName, string lastName, Department department, string phoneNumber, string mobile, string email, string photoPath)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Responsible
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
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

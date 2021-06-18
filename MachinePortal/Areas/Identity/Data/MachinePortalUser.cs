using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using Microsoft.AspNetCore.Identity;

namespace MachinePortal.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the MachinePortalUser class
    public class MachinePortalUser : IdentityUser
    {
        [PersonalData]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [PersonalData]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [PersonalData]
        [Phone]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [PersonalData]
        [Display(Name = "Department")]
        public Department Department { get; set; }

        [PersonalData]
        [Display(Name = "Job Role")]
        public string JobRole { get; set; }

        [Display(Name = "Photo")]
        public string PhotoPath { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}

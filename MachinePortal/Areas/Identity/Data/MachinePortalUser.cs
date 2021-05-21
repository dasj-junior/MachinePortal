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
        public string FirstName { get; set; }

        [PersonalData]
        public string LastName { get; set; }

        [PersonalData]
        [Phone]
        public string Mobile { get; set; }

        [PersonalData]
        public Department Department { get; set; }

        [PersonalData]
        public string JobRole { get; set; }

        public string PhotoPath { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Areas.Identity.Data
{
    public class Permission
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Permission Name")]
        public string PermissionName { get; set; }
        [Display(Name = "User Permissions")]
        public ICollection<UserPermission> UserPermissions { get; set; }

        public Permission()
        {
        }

        public Permission(int Id, string permissionName)
        {
            ID = Id;
            PermissionName = permissionName;
        }
    }
}

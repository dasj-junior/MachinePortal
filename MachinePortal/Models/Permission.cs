using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class Permission
    {
        public int ID { get; set; }
        public string PermissionName { get; set; }
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

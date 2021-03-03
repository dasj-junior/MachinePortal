using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Areas.Identity.Data;

namespace MachinePortal.Areas.Identity.Data
{
    public class UserPermission
    {
        public string UserID { get; set; }
        public int PermissionID { get; set; }

        public MachinePortalUser MachinePortalUser { get; set; }
        public Permission Permission { get; set; }
    }
}

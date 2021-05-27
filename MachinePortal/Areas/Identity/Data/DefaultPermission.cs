using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachinePortal.Areas.Identity.Data
{
    public class DefaultPermission
    {
        public int ID { get; set; }
        public Permission defaultPermission { get; set; }

        public DefaultPermission()
        {
        }
    }
}

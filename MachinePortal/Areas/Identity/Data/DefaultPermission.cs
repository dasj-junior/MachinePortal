using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachinePortal.Areas.Identity.Data
{
    public class DefaultPermission
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        [Display(Name = "Default Permission")]
        public Permission defaultPermission { get; set; }

        public DefaultPermission()
        {
        }
    }
}

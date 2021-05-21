using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MachinePortal.Areas.Identity.Data
{
    public class DepartmentRead
    {
        public int ID { get; set; }
        public Department ReadDepartment { get; set; }

        public Department Department { get; set; }

        public DepartmentRead()
        {
        }
    }
}

namespace MachinePortal.Areas.Identity.Data
{
    public class DepartmentWrite
    {
        public int ID { get; set; }
        public Department WriteDepartment{ get; set; }

        public Department Department { get; set; }

        public DepartmentWrite()
        {
        }
    }
}

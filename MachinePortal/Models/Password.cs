using MachinePortal.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachinePortal.Models
{
    public class Password
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        [Display(Name = "Equipment Name")]
        public string EquipmentName { get; set; }
        
        [Display(Name = "Equipment Description")]
        public string EquipmentDescription { get; set; }

        [Display(Name = "User")]
        public string User { get; set; }
        
        [Display(Name = "Password")]
        public string Pass { get; set; }

        [Display(Name = "Level")]
        public string Level { get; set; }

        [Display(Name = "Department ID")]
        public int DepartmentID { get; set; }
        [Display(Name = "Department")]
        public string DepartmentName { get; set; }

        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }
        [Display(Name = "Machine")]
        public string MachineName { get; set; }

        public Password()
        {
        }

        public Password(int Id, string equipmentName, string equipmentDescription, string user, string pass, string level, int departmentID, int machineID)
        {
            ID = Id;
            EquipmentName = equipmentName;
            EquipmentDescription = equipmentDescription;
            User = user;
            Pass = pass;
            Level = level;
            DepartmentID = departmentID;
            MachineID = machineID;
        }
    }
}

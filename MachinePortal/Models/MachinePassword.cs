using MachinePortal.Areas.Identity.Data;

namespace MachinePortal.Models
{
    public class MachinePassword
    {
        public int ID { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentDescription { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Level { get; set; }

        public Department Department { get; set; }
        public Machine Machine { get; set; }

        public MachinePassword()
        {
        }
    }
}

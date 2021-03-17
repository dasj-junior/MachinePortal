using MachinePortal.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineComment
    {
        public int ID { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public int MachineID { get; set; }
        public Machine Machine { get; set; }

        public string UserID { get; set; }
        public MachinePortalUser User { get; set; }

        public MachineComment()
        {
        }

        public MachineComment(int iD, string author, string comment, DateTime date)
        {
            ID = iD;
            Comment = comment;
            Date = date;
        }
    }
}

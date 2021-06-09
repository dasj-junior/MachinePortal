using MachinePortal.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace MachinePortal.Models
{
    public class MachineComment
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }
        [Display(Name = "Machine")]
        public Machine Machine { get; set; }

        [Display(Name = "User ID")]
        public string UserID { get; set; }
        [Display(Name = "User")]
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

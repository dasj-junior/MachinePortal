using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Models
{
    public class MachineComment
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        public MachineComment()
        {
        }

        public MachineComment(int iD, string author, string comment, DateTime date)
        {
            ID = iD;
            Author = author;
            Comment = comment;
            Date = date;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Ticket
{
    public class TicketDetail
    {
        public int Id {get; set; }
        public string Title {get; set; }
        public string Description {get; set; }
        public bool Archived {get; set;}
        public int CategoryId {get; set; }
        public int UserID {get; set; }
    }
}
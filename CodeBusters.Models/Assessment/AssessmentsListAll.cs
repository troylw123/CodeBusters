using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Assessment
{
    public class AssessmentsListAll
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public int TimeRequired { get; set; }
        public decimal Cost { get; set; }
        public bool Accepted { get; set; }
        public int TicketId { get; set; }
    }
}
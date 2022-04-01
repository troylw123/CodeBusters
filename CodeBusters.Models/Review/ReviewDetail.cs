using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Review
{
    public class ReviewDetail
    {
        public int Id { get; set; }
        public float Rating { get; set; }
        public string Comments { get; set; }
        public int TicketId { get; set; }
    }
}
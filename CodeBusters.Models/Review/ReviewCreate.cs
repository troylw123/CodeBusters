using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Review
{
    public class ReviewCreate
    {
        [Required]
        [Range(0, 5)]
        public float Rating { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public int TicketId { get; set; }
    }
}
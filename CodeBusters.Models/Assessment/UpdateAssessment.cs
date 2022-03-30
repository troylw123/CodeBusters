using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Assessment
{
    public class UpdateAssessment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Comments { get; set; }
        [Required]
        public int TimeRequired { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public bool Accepted { get; set; }
        [Required]
        public int TicketId { get; set; }
    }
}
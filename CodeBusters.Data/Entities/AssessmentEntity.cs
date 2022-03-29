using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Data.Entities
{
    public class AssessmentEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string comments { get; set; }
        [Required]
        public int timeRequired { get; set; }
        [Required]
        public decimal cost { get; set; }
        [Required]
        public bool accepted { get; set; }
        [ForeignKey("Id")]
        public int ticketId { get; set; }
        public TicketEntity TicketEntity { get; set; }
    }
}
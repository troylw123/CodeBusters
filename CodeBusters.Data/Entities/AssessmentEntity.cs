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
        public string Comments { get; set; }
        [Required]
        public int TimeRequired { get; set; }
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public bool Accepted { get; set; }
        [ForeignKey("Id")]
        public int TicketId { get; set; }
        public TicketEntity TicketEntity { get; set; }
    }
}
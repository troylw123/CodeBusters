using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Data.Entities
{
    public class ReviewEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, 5)]
        public float Rating { get; set; }
        [Required]
        public string comments { get; set; }
        [ForeignKey("Id")]
        public int TicketId { get; set; }
        public TicketEntity TicketEntity { get; set; }
    }
}
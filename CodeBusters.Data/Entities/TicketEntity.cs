using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Data.Entities
{
    public class TicketEntity
    {
         [Key]
         [Required]
        public int Id {get; set;}
        [Required]
        public string Title {get; set; }
        [Required]
        public string Description {get; set; }
        [Required]
        public bool Archived {get; set;}

        [Required]
        public int Category {get; set; }
        [Required]
        public int UserID {get; set; }
    }
}
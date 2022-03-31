using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Data.Entities
{
    public class ResponseEntity
    {
         [Key]
        public int Id {get; set;}
        [Required]
        public string Text {get; set;}
        [ForeignKey("Assessment")]
        public int AssessmentId {get; set;}

        // public AssessmentEntity Assessment {get; set;}
    }
}
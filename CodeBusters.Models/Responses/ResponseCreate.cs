using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Responses
{
    public class ResponseCreate
    {
        [Required]
        [MinLength(20, ErrorMessage ="{0} must be at least {1} characters long.")]
        [MaxLength(200, ErrorMessage ="{0} must contain no more than {1} characters.")]
        public string Text {get; set;}
        [Required]
        public int AssessmentId {get; set;}
    }
}
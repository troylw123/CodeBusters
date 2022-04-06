using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Responses
{
    public class ResponseListItem
    {
        public int Id {get; set;}
        
        public string Text {get; set;}
        
        public int AssessmentId {get; set;}
    }
}
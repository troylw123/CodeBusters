using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Data.Entities
{
    public class UserEntity
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string FullName {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
        
        [DefaultValue("false")]
        public bool isAdmin {get; set;}

        public List<TicketEntity> Tickets {get; set;} //foreign key work
    }

}
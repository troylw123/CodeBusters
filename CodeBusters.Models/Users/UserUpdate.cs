using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Users
{
    public class UserUpdate
    {
        [Required]
        public int Id {get; set;}
        [Required]
        [MinLength(2)]
        public string FullName {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [MinLength(8)]
        public string Password {get; set;}
        [Required]
        [Compare("Password")]
        public string ConfirmPassword {get; set;}
    }
}
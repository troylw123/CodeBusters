using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeBusters.Models.Category
{
    public class CategoryCreate
    {
        [Key]
        [Required]
        public int Id {get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1,10)]
        public int Difficulty {get; set; }
    }
}
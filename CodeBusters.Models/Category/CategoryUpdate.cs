using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Category
{
    public class CategoryUpdate
    {
        [Key]
        [Required]
        public int Id {get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public int Difficulty {get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeBusters.Models.Category
{
    public class CategoryCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public int Difficulty {get; set; }
    }
}
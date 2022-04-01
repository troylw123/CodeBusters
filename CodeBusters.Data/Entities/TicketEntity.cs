using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Data.Entities
{
    public class TicketEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public CategoryEntity Category { get; set; } //foreign key work
        public UserEntity User { get; set; } //foreign key work
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DefaultValue("false")]
        public bool isArchived { get; set; }
        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
    }
}
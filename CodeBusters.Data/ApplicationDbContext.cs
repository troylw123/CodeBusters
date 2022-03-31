using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBusters.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeBusters.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<AssessmentEntity> Assessments { get; set; }
        public DbSet<ResponseEntity> Responses { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using MvcMovie.Models;
using DemoMvcProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoMvcProject.DAL
{
    public class SchoolContext : IdentityDbContext<User>
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            
        }

        public DbSet<StudentModel> Students { get; set; }

        
    }
}
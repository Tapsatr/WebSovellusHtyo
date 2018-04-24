using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HTyo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HTyo.Data
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }

        public DbSet<JobOrder> JobOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<JobOrder>().ToTable("JobOrder");
        }
        public DbSet<HTyo.Models.User> User { get; set; }

    }
}

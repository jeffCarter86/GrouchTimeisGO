using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrashMan.Models;

namespace TrashMan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Employee",
                NormalizedName = "EMPLOYEE"
            },
             new IdentityRole
             {
                 Name = "Customer",
                 NormalizedName = "CUSTOMER"
             }
            );
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerSchedPickUp> CustomerSchedPickUps { get; set; }
        public DbSet<ConfirmedPU> ConfirmedPUs { get; set; }
    }
}

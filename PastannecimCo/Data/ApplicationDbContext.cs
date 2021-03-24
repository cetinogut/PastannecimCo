using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PastannecimCo.Data.EntityConfigurations;
using PastannecimCo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PastannecimCo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<CakeOrder> CakeOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CakeOrderEntityConfiguration.Configure(modelBuilder);
            UserEntityConfiguration.Configure(modelBuilder);
        }
    }
}

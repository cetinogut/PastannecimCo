using Microsoft.EntityFrameworkCore;
using PastannecimCo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Data.EntityConfigurations
{
    public class CakeOrderEntityConfiguration
    {
        internal static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CakeOrder>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Price)
                    .HasColumnType("decimal");

                e.Property(c => c.UserId)
                    .IsRequired();

                e.HasOne(c => c.User)
                    .WithMany(u => u.CakeOrders)
                    .HasForeignKey(c => c.UserId);
            });
        }
    }
}

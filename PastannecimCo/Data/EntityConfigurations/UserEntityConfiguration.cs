using Microsoft.EntityFrameworkCore;
using PastannecimCo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastannecimCo.Data.EntityConfigurations
{
    public class UserEntityConfiguration
    {
        internal static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(e => e.Id);
            });


        }
    }
}

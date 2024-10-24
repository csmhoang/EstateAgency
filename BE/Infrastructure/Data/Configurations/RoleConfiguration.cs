﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        // modelBuilder.ApplyConfiguration(new RoleConfiguration());
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Name = "landlord",
                    NormalizedName = "LANDLORD"
                },
                new Role
                {
                    Name = "tenant",
                    NormalizedName = "TENANT"
                }
            );
        }
    }
}

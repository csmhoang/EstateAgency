using Core.Entities;
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
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role
                {
                    Name = "Landlord",
                    NormalizedName = "LANDLORD"
                },
                new Role
                {
                    Name = "Tenant",
                    NormalizedName = "TENANT"
                }
            );
        }
    }
}

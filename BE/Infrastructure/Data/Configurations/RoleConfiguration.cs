using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    // modelBuilder.ApplyConfiguration(new RoleConfiguration());
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = "1a2b3c4d-1234-5678-9101-abcdefabcdef",
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "fd1155b7-f189-4b64-aa6b-9d79f7ca812a"
            },
            new Role
            {
                Id = "2b3c4d5e-2345-6789-1011-bcdefabcdef0",
                Name = "landlord",
                NormalizedName = "LANDLORD",
                ConcurrencyStamp = "73497676-8833-4157-a96e-a191532be901"
            },
            new Role
            {
                Id = "3c4d5e6f-3456-7891-0112-cdefabcdef01",
                Name = "tenant",
                NormalizedName = "TENANT",
                ConcurrencyStamp = "86966169-88e8-4a90-b74a-552767a238ea"
            }
        );
    }
}

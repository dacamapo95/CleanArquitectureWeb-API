using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAchitecture.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder
            .HasData(
                new IdentityRole
                {
                    Id = "7d9f91b4-1631-4d46-88fe-de6a081bdd9c",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = "0ae0959f-1425-4346-be1d-7872c2a10b70",
                    Name = "Operator",
                    NormalizedName = "OPERATOR"
                });
    }
}

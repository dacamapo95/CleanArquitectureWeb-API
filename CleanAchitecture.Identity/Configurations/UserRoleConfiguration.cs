using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAchitecture.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder
            .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "7d9f91b4-1631-4d46-88fe-de6a081bdd9c",
                    UserId = "80126bc1-4cc2-4e92-9012-db4ebe60d8a2",
                },
                new IdentityUserRole<string>()
                {
                    RoleId = "0ae0959f-1425-4346-be1d-7872c2a10b70",
                    UserId = "c266c269-734f-46d6-9423-82907f7009d3",
                }
            );
    }
}

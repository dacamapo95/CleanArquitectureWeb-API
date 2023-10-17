using CleanAchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAchitecture.Identity.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
        new ApplicationUser
        {
            //Admin
            Id = "80126bc1-4cc2-4e92-9012-db4ebe60d8a2",
            Email = "danielcami782@hotmail.com",
            Name = "Dani",
            LastNames = "Males Poveda",
            UserName = "dacamapo",
            NormalizedUserName = "dacamapo",
            PasswordHash = hasher.HashPassword(null, "Dani*12345"),
            EmailConfirmed = true
        },
        new ApplicationUser
        {
            Id = "c266c269-734f-46d6-9423-82907f7009d3",
            Email = "pepito@hotmail.com", //Admin
            Name = "Pepe",
            LastNames = "Perez",
            UserName = "pepitoperez",
            NormalizedUserName = "pepitoperez",
            PasswordHash = hasher.HashPassword(null, "pepito*12345"),
            EmailConfirmed = true,
        });
    }
}

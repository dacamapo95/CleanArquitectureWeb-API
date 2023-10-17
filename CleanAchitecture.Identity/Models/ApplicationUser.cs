using Microsoft.AspNetCore.Identity;

namespace CleanAchitecture.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }

    public string LastNames { get; set; }
}

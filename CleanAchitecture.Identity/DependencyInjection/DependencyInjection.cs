using CleanAchitecture.Identity.Models;
using CleanAchitecture.Identity.Persistence;
using CleanAchitecture.Identity.Services;
using CleanArquitecture.Application.Contracts.Identity;
using CleanArquitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanAchitecture.Identity.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"),
            sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                //ValidIssuer = configuration["JwtSettings: Issuer"],
                //ValidAudience = configuration["JwtSettings: Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings: Key"]))
            };
        });

        return services;
    }
}

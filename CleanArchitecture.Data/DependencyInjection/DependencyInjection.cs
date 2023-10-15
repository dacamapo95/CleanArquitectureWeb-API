using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArquitecture.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
                                                               IConfiguration configuration)
    {
        services.AddDbContext<StreamerDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IVideoRepository, VideoRepository>();
        services.AddScoped<IStreamerRepository, StreamerRepository>();

        return services;
    }
}
